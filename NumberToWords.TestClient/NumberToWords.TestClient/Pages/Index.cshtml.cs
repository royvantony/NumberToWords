using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NumberToWords.Models;
using NumberToWords.Services;

namespace NumberToWords.TestClient.Pages
{
    public class IndexModel : PageModel
    {
        private readonly INumToWordsRepository numToWordsRepository;

        [BindProperty]
        public NumToWords NumToWords { get; set; }

        public IndexModel(INumToWordsRepository numToWordsRepository)
        {
            this.numToWordsRepository = numToWordsRepository;
        }

        public async void OnPost()
        {
            if(ModelState.IsValid)
            {
                NumToWords = await numToWordsRepository.getNumberInWords(NumToWords.number);
            }
        }
    }
}