using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
//using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace Qc4Launcher.Forms.Cross_Tabulation
{
    public class TabBinding
    {
        public static ObservableCollection<VMTabItems> tabItems { get; set; }

        EditTabulation tab;

        public TabBinding()
        {
            tabItems = new ObservableCollection<VMTabItems>();
        }



        public void addItem(bool isVertical = false)
        {
            VMTabItems _tabItem;
            TabContent tbcontent;
            GraphOptions gpDesign;

            _tabItem = new VMTabItems();
            int count = tabItems.Count + 1;
            foreach (VMTabItems item in tabItems)
            {
                if (item.Header == LocalResource.CROS_TABULATION_TAB + " " + count.ToString())
                {
                    count++;
                }
            }
            _tabItem.Tabid = count;
            tbcontent = new TabContent();
            tbcontent.Name = "tabs" + count.ToString();
            gpDesign = new GraphOptions();
            gpDesign.Name = "tabs" + count.ToString();
            _tabItem.TabConten = tbcontent;
            _tabItem.gpDesign = gpDesign;
            _tabItem.lst = new ObservableCollection<CrossQuestionSetting>();
            _tabItem.Header = LocalResource.CROS_TABULATION_TAB + " " + count.ToString();
            _tabItem.isactive = true;
            _tabItem.OnorOff = LocalResource.CELL_ON;
            _tabItem.gpDesign._data.Clear();
            _tabItem.gpDesign.datasetting_copy.Clear();
            _tabItem.VerticalOnOrOFF = isVertical ? true : false; ;
            tabItems.Add(_tabItem);

            ICollectionView collectionView1 = CollectionViewSource.GetDefaultView(tabItems);

            if (collectionView1 != null)
            {
                collectionView1.MoveCurrentTo(_tabItem);
            }

        }
        public void updateTabitems()
        {

        }
        //public void removeTabs()
        //{
        //    tab = new EditTabulation(tabItems);
        //    tab.ShowDialog();
        //}
    }

    public class VMTabItems : INotifyPropertyChanged
    {
        private ObservableCollection<CrossQuestionSetting> _lst { get; set; }
        private TabContent _tabConten { get; set; }
        private Brush _brushes { get; set; }
        private GraphOptions _gpDesign { get; set; }
        private string _header { get; set; }
        private bool _isactive { get; set; }
        private string _onoroff { get; set; }
        private int _tabid { get; set; }
        public bool _rdgt { get; set; }
        private bool _VerticalOnOrOFF { get; set; }

        public bool VerticalOnOrOFF
        {
            get
            {
                return _VerticalOnOrOFF;
            }
            set
            {
                _VerticalOnOrOFF = value;
                NotifyPropertyChanged();
            }
        }
        private int _id { get; set; }
        public int id
        {
            get { return _id; }
            set { _id = value; }
        }
        public bool rdgt
        {
            get
            {
                return _rdgt;
            }
            set
            {
                _rdgt = value;
                NotifyPropertyChanged();
            }
        }
        public ObservableCollection<CrossQuestionSetting> lst
        {
            get
            {
                return _lst;
            }
            set
            {
                _lst = value;

                NotifyPropertyChanged();
            }
        }
        public Brush _Brushes
        {
            get
            {
                if (_isactive == false)
                {

                    _brushes = (SolidColorBrush)(new BrushConverter().ConvertFrom("#999999"));
                    return _brushes;
                }
                else
                {
                    _brushes = (SolidColorBrush)(new BrushConverter().ConvertFrom("#FF333333"));
                    return _brushes;
                }
            }
            set
            {
                _brushes = value;
                NotifyPropertyChanged();
            }
        }
        public GraphOptions gpDesign
        {
            get { return _gpDesign; }
            set { _gpDesign = value; }
        }
        public int Tabid
        {
            get { return _tabid; }
            set { _tabid = value; }
        }
        public TabContent TabConten
        {
            get { return _tabConten; }
            set { _tabConten = value; }
        }
        public string OnorOff
        {
            get
            {
                if (_isactive == false)
                {


                    return _onoroff;
                }
                else
                {

                    return _onoroff;
                }
            }
            set
            {
                _onoroff = value;
                NotifyPropertyChanged();
            }

        }
        public string Header
        {
            get
            {
                return _header;
            }
            set
            {
                _header = value;
            }
        }
        public bool isactive
        {
            get
            {
                return _isactive;
            }
            set
            {
                _isactive = value;
                NotifyPropertyChanged();
            }
        }
        public VMTabItems()
        {

        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (propertyName == "lst")
            {
                this.gpDesign.getdata();
            }
            if (propertyName == "rdgt")
            {
                this.gpDesign.disables();
            }
            if (propertyName == "VerticalOnOrOFF")
            {
                TabConten.Vertical();
            }
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
