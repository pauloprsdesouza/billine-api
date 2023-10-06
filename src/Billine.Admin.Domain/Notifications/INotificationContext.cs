using System.Collections.Generic;

namespace Billine.Admin.Domain.Notifications
{
    public interface INotificationContext
    {
        List<string> GetValidationErrors();
        List<string> GetNotFoundErrors();
        public List<string> GetForbiddenErrors();
        bool AreThereForbiddenErros();
        bool AreThereValidationErrors();
        bool AreThereNotFoundErros();
        void AddValidationError(string message);
        void AddNotFoundError(string message);
        void AddForbiddenError(string message);
    }
}
