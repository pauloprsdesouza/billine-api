using AutoMapper;
using Billine.Admin.Domain.Orders;
using Billine.Admin.Domain.Products;
using Billine.Admin.Domain.Sefaz;
using Billine.Admin.Domain.Sefaz.Models;
using HtmlAgilityPack;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Billine.Admin.Infrastructure.Apis
{
    public class SefazProvider : ISefazProvider
    {
        private readonly HttpClient _client;
        private readonly ILogger<SefazProvider> _logger;
        private readonly CultureInfo brazilianCulture = new CultureInfo("pt-BR");
        private readonly IMapper _mapper;

        public SefazProvider(HttpClient client, ILogger<SefazProvider> logger, IMapper mapper)
        {
            _client = client;
            _logger = logger;
            _mapper = mapper;
        }

        private decimal ParseDecimal(string text)
        {
            return decimal.Parse(text, NumberStyles.Currency, brazilianCulture);
        }

        private string ExtractNumericId(string idText)
        {
            // Extracting numeric values using regex
            var match = Regex.Match(idText, @"\d+");
            return match.Success ? match.Value : string.Empty;
        }

        private string ExtractCNPJ(HtmlNode documentNode)
        {
            // XPath query to select the text node containing the CNPJ information.
            var cnpjNode = documentNode.SelectSingleNode("//div[@data-role='content']//div[@class='txtCenter']//div[contains(text(), 'CNPJ')]");

            if (cnpjNode != null)
            {
                // Regular expression to extract CNPJ.
                var match = Regex.Match(cnpjNode.InnerText.Trim(), @"CNPJ:\s*([\d]{2}\.[\d]{3}\.[\d]{3}/[\d]{4}-[\d]{2})");

                if (match.Success)
                {
                    // Return the extracted CNPJ.
                    return match.Groups[1].Value;
                }
            }

            throw new FormatException("CNPJ format was not as expected or node not found.");
        }


        private DateTime ExtractDateTime(HtmlNode documentNode)
        {
            // XPath query to select the text node containing the date and time information.
            var dateTimeNode = documentNode.SelectSingleNode("//div[@data-role='collapsible']//ul[@data-role='listview']/li/strong[contains(text(), 'Data de Emiss')]/following-sibling::text()[1]");

            if (dateTimeNode != null)
            {
                // Extract the date and time part before " - Via Consumidor"
                var match = Regex.Match(dateTimeNode.InnerText.Trim(), @"(\d{2}/\d{2}/\d{4} \d{2}:\d{2}:\d{2})");

                if (match.Success)
                {
                    // Parse and return the date and time using Brazilian culture format.
                    return DateTime.ParseExact(match.Value, "dd/MM/yyyy HH:mm:ss", brazilianCulture);
                }
            }

            throw new FormatException("Date and time format was not as expected or node not found.");
        }

        public async Task<Order> GetNFCEs(string key)
        {
            try
            {
                var response = await _client.GetAsync($"?p={key}");
                response.EnsureSuccessStatusCode();

                var htmlContent = await response.Content.ReadAsStringAsync();

                var doc = new HtmlDocument();
                doc.LoadHtml(htmlContent);

                var nfce = new NFCe
                {
                    CompanyName = doc.DocumentNode.SelectSingleNode("//div[@class='txtTopo']").InnerText.Trim(),
                    CreatedAt = ExtractDateTime(doc.DocumentNode),
                    CompanyCNPJ = ExtractCNPJ(doc.DocumentNode),
                    Items = new List<NFCeItem>()
                };

                var itemsNodes = doc.DocumentNode.SelectNodes("//tr[starts-with(@id, 'Item')]");

                foreach (var itemNode in itemsNodes)
                {
                    var descriptionText = itemNode.SelectSingleNode(".//span[@class='txtTit']").InnerText.Trim();
                    var unityMeasureText = itemNode.SelectSingleNode(".//span[@class='RUN']").InnerText.Trim();
                    bool isKg = unityMeasureText.Contains("KG");

                    // Remove UN or KG from the description
                    var description = Regex.Replace(descriptionText, @"\sUN|\sKG", "", RegexOptions.IgnoreCase);

                    var item = new NFCeItem
                    {
                        Description = description,
                        ExternalId = ExtractNumericId(itemNode.SelectSingleNode(".//span[@class='RCod']").InnerText.Trim()),
                        Quantity = ParseDecimal(itemNode.SelectSingleNode(".//span[@class='Rqtd']").InnerText.Replace("Qtde.:", "").Trim()),
                        UnityMeasure = isKg ? UnityMeasure.KG : UnityMeasure.UN,
                        UnityPrice = ParseDecimal(itemNode.SelectSingleNode(".//span[@class='RvlUnit']").InnerText.Replace("Vl. Unit.:", "").Replace("&nbsp;", "").Trim()),
                        TotalPrice = ParseDecimal(itemNode.SelectSingleNode(".//td/span[@class='valor']").InnerText.Trim())
                    };

                    nfce.Items.Add(item);
                }

                return _mapper.Map<Order>(nfce);
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "UNABLE_TO_GET_CATEGORIES");
                return null;
            }
        }
    }
}
