using NumberToWords.Models;

namespace NumberToWords.Services
{
    public interface INumToWordsRepository
    {
       Task<NumToWords> getNumberInWords(string number);
    }
}