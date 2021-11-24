using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebPaymentsApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public string Message { get; private set; } = "";

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        /*
        public void OnGet()
        {
            Message += $" Server time is { DateTime.Now }";
        }
        */
        public void OnPostSubmit() {
            Message = "asdasdasdasdasd!!!!!!!!!!!!!!!!!";
        }
        public void OnPost()
        {
            Message = "Form Posted";
        }
        public void OnPostDelete()
        {
            Message = "Delete handler fired";
        }
        public void OnPostEdit(int id)
        {
            Message = "Edit handler fired";
        }
        public void OnPostView(int id)
        {
            Message = "View handler fired";
        }
        public void OnPostMobilePayments() {
            Redirect("./Error");
            //Message = "WOOHOOOOOO";
        }
    }
}