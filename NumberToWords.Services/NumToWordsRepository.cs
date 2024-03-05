using NumberToWords.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberToWords.Services
{
    public class NumToWordsRepository : INumToWordsRepository
    {
        private NumToWords _numToWords;
        async Task<NumToWords> INumToWordsRepository.getNumberInWords(string number)
        {
            _numToWords = new NumToWords();
            _numToWords.number = number;
            _numToWords.words = NumToWords.ConvertNumToWords(number);
            return  _numToWords;
        }
    }
}
