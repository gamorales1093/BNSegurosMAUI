using System;
using Prism.Mvvm;
using System.Collections.Generic;

namespace BNSegurosMAUI.Models
{
    public class ItemGroupQuestion : BindableBase
    {
        public QuestionType QuestionType { get; set; }

        private List<FrequentlyAskedQuestion> _itemQuestions;
        public List<FrequentlyAskedQuestion> ItemQuestions
        {
            get => _itemQuestions;
            set => SetProperty(ref _itemQuestions, value);
        }


        private bool _isCellExpanded;
        public bool IsCellExpanded
        {
            get => _isCellExpanded;
            set => SetProperty(ref _isCellExpanded, value);
        }

        private int _itemCount;
        public int ItemCount
        {
            get => ItemQuestions.Count;
            set => SetProperty(ref _itemCount, value);
        }
    }
}
