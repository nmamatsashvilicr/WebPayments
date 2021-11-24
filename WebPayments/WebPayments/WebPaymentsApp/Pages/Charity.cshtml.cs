using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Globalization;

namespace WebPaymentsApp.Pages
{
    public class CharityModel : PageModel
    {
        public string FormMessage { get; set; }
        public IActionResult OnGet()
        {
            return Page();
        }
        public void OnpostSubmit(classes.Charity data)
        {
            try
            {
                string personalId = data.PersonalId;
                string phone = data.MobilePhone;
                double amount = double.Parse(data.Amount, CultureInfo.InvariantCulture);
                double commisionFee = WebPaymentsApp.Shared.calculateCommision(amount);
                if (!WebPaymentsApp.Shared.isValidPhone(phone))
                {
                    FormMessage = "მობილური ნომერი არ არის სწორი ფორმატით";
                    return;
                }
                if (!WebPaymentsApp.Shared.isValidPin(personalId)) {
                    FormMessage = "პირადი ნომერი არ არის სწორი ფორმატით";
                    return;
                }
                if (!WebPaymentsApp.Shared.isValidAmount(amount))
                {
                    FormMessage = "არასწორი თანხა";
                    return;
                }

                Dictionary<string, string> parameters = new Dictionary<string, string> {
                    { "MobilePhone", phone },
                    { "Amount", amount.ToString(CultureInfo.InvariantCulture) },
                    { "FeeAmount", commisionFee.ToString(CultureInfo.InvariantCulture) },
                    { "PersonalId", personalId }
                };
                string query = $@"
                                INSERT INTO WebPayments.dbo.WebPaymentsData ( ServiceName, MobilePhone, Amount, FeeAmount, PersonalId, Iban, RegDate) 
                                VALUES( 'Charity Payments', @MobilePhone, @Amount, @FeeAmount, @PersonalId, '', GETDATE())";
                int rst = DB.Run(query, parameters);
                if(rst < 0)
                    FormMessage = "მონაცემთა შენახვა ვერ მოხერხდა";
                else 
                    FormMessage = "მონაცემები შენახულია";

            }
            catch (Exception ex)
            {
                FormMessage = ex.Message;
            }
        }
    }
}
