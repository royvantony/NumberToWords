using NumberToWords.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NumberToWords.Services
{
    public class NumToWordsApiRepository : INumToWordsRepository
    {
        private readonly HttpClient httpClient;
        private NumToWords _numToWords;

        public NumToWordsApiRepository(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        async Task<NumToWords> INumToWordsRepository.getNumberInWords(string number)
        {
            _numToWords = new NumToWords();
            _numToWords.number = number;
            _numToWords.words = await httpClient.GetStringAsync($"api/NumToWords/{number}");
            return _numToWords;
        }
    }
}

