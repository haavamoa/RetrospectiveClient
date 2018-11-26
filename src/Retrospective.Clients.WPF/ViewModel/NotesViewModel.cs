using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Data;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Retrospective.Clients.WPF.ViewModel.Interfaces;
using Retrospective.Service.DataModels;

namespace Retrospective.Clients.WPF.ViewModel
{  
    public class NotesViewModel<T> : ViewModelBase where T : class, new()
    {
        private List<T> m_notes;
        private bool m_isFocused;

        public ObservableCollection<T> Notes { get; set; }

        public bool IsFocused
        {
            get => m_isFocused;
            set => Set(ref m_isFocused, value);
        }

        public void Initialize(List<T> notes)
        {
            m_notes = notes;
            Notes = new ObservableCollection<T>(m_notes);
        }
    }
}