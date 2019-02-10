using System.Threading.Tasks;
using RecipiecesWeb.Models;

namespace RecipiecesWeb.Services
{
    public interface IAlertService
    {
        AlertControlViewModel AlertData { get; set; }
        void SetAlert(string title, string message, string kindOfAlert = "info");
        Task ClearAlert();
    }
}