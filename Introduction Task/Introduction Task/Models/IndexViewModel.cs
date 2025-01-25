using Introduction_Task.Entities;
using System.Collections;
using System.Collections.Generic;

namespace Introduction_Task.Models
{
    public class IndexViewModel
    {
        public IEnumerable<Drink> Drink { get; set; }
        public IEnumerable<FastFood> FastFood { get; set; }
        public IEnumerable<HotMeal> HotMeal { get; set; }
    }
}
