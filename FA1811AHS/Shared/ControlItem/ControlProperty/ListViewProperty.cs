using System.Drawing;
using System.Windows.Forms;

namespace FA1811AHS.Shared
{
    public static class ListViewProperty
    {
        public static void SetProperty(ListView listView)
        {
            listView.Font = new Font("微軟正黑體", 10F);
            listView.GridLines = true;
            listView.Scrollable = true;
            listView.FullRowSelect = true;
            listView.View = View.Details;
        }
    }
}