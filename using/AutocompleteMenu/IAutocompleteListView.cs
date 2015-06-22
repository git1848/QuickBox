using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AutocompleteMenuNS
{
    /// <summary>
    /// Control for displaying menu items, hosted in AutocompleteMenu.
    /// </summary>
    public interface IAutocompleteListView
    {
        /// <summary>
        /// Image list
        /// </summary>
        ImageList ImageList { get; set; }

        /// <summary>
        /// Index of current selected item
        /// </summary>
        int SelectedItemIndex { get; set; }

        /// <summary>
        /// List of visible elements
        /// </summary>
        IList<AutocompleteItem> VisibleItems { get;set;}

        /// <summary>
        /// Occurs when user selected item for inserting into text
        /// </summary>
        event EventHandler ItemSelected;
    }
}
