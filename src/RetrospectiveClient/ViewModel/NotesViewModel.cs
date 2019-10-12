using System.Collections.Generic;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;

namespace RetrospectiveClient.ViewModel
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