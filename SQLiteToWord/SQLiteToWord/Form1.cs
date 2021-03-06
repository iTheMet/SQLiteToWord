using SQLiteToWord.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SQLiteToWord
{
    public partial class Form1 : Form
    {
        ApplicationContext db;

        List<Products> productsInBasket;
        List<Products> productsInStock;
        public Form1()
        {
            InitializeComponent();
            db = new ApplicationContext();

            productsInBasket = new List<Products>();
            productsInStock = new List<Products>();

            listView1.FullRowSelect = true;
            listView2.FullRowSelect = true;

            listView1.Columns.Add("Название товара");
            listView1.Columns.Add("Цена");
            listView1.Columns.Add("Кол-во на складе");

            listView2.Columns.Add("Название товара");
            listView2.Columns.Add("Цена");
            listView2.Columns.Add("Кол-во на складе");

            updateProducts();
        }
        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView2.Focused == true)
            {
                if (Convert.ToInt32(listView2.FocusedItem.SubItems[2].Text) >= 0)
                {
                    for (int i = 0; i < productsInStock.Count; i++)
                    {
                        if (Convert.ToInt32(listView2.FocusedItem.SubItems[3].Text) == productsInStock[i].product_id)
                        {
                            if (productsInBasket.Count > 0)
                            {
                                for (int j = 0; j < productsInBasket.Count; j++)
                                {
                                    if (Convert.ToInt32(listView2.FocusedItem.SubItems[3].Text) == productsInBasket[j].product_id)
                                    {
                                        productsInStock[i].number--;
                                        productsInBasket[j].number++;
                                        break;
                                    }
                                    if(j == productsInBasket.Count - 1)
                                    {
                                        Products np = new Products()
                                        {
                                            product_id = productsInStock[i].product_id,
                                            product_name = productsInStock[i].product_name,
                                            price_per_one = productsInStock[i].price_per_one,
                                            warehouse_place = productsInStock[i].warehouse_place,
                                            number = 1
                                        };
                                        productsInBasket.Add(np);
                                        productsInStock[i].number--;
                                        break;
                                    }
                                }
                            }
                            else
                            {   
                                Products np = new Products() 
                                {
                                    product_id = productsInStock[i].product_id, 
                                    product_name = productsInStock[i].product_name,
                                    price_per_one = productsInStock[i].price_per_one,
                                    warehouse_place = productsInStock[i].warehouse_place,
                                    number = 1
                                };
                                productsInBasket.Add(np);
                                productsInStock[i].number--;
                                break;
                            }
                        }
                    }
                }
                updateProducts();
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.Focused == true)
            {
                if (Convert.ToInt32(listView1.FocusedItem.SubItems[2].Text) >= 0)
                {
                    for (int i = 0; i < productsInBasket.Count; i++)
                    {
                        if (Convert.ToInt32(listView1.FocusedItem.SubItems[3].Text) == productsInBasket[i].product_id)
                        {
                                for (int j = 0; j < productsInStock.Count; j++)
                                {
                                    if (Convert.ToInt32(listView1.FocusedItem.SubItems[3].Text) == productsInStock[j].product_id)
                                    {
                                        productsInBasket[i].number--;
                                        productsInStock[j].number++;

                                        if (productsInBasket[i].number < 1)
                                        {
                                            productsInBasket.RemoveAt(i);
                                        }
                                        break;
                                    }
                                }
                        }
                    }
                }
                updateProducts();
            }
        }

            private void updateProducts()
        {
            productsInStock = db.Products.ToList();

            listView1.Items.Clear();
            listView2.Items.Clear();

            foreach(Products pd in productsInStock)
            {
                if (pd.number > 0)
                {
                    ListViewItem prName = new ListViewItem(pd.product_name);
                    ListViewItem.ListViewSubItem prPrice = new ListViewItem.ListViewSubItem(prName, pd.price_per_one.ToString());
                    ListViewItem.ListViewSubItem prInStock = new ListViewItem.ListViewSubItem(prName, pd.number.ToString());
                    ListViewItem.ListViewSubItem prId = new ListViewItem.ListViewSubItem(prName, pd.product_id.ToString());

                    prName.SubItems.Add(prPrice);
                    prName.SubItems.Add(prInStock);
                    prName.SubItems.Add(prId);
                    listView2.Items.Add(prName);
                }
            }

            foreach (Products pb in productsInBasket)
            {
                if (pb.number > 0)
                {
                    ListViewItem prName = new ListViewItem(pb.product_name);
                    ListViewItem.ListViewSubItem prPrice = new ListViewItem.ListViewSubItem(prName, pb.price_per_one.ToString());
                    ListViewItem.ListViewSubItem prInStock = new ListViewItem.ListViewSubItem(prName, pb.number.ToString());
                    ListViewItem.ListViewSubItem prId = new ListViewItem.ListViewSubItem(prName, pb.product_id.ToString());

                    prName.SubItems.Add(prPrice);
                    prName.SubItems.Add(prInStock);
                    prName.SubItems.Add(prId);
                    listView1.Items.Add(prName);
                }
            }

        }  

        private void button3_Click(object sender, EventArgs e)
        {
            //TODO сохранение в бд
            if (productsInBasket.Count > 0) {
                MessageBoxButtons mbb = MessageBoxButtons.YesNo;
                DialogResult result;
                
                result = MessageBox.Show("Вы уверены что хотите оформить заказ?", "Заказ", mbb);
                if (result == DialogResult.Yes)
                {
                    Orders order = new Orders
                    {
                        order_id = db.Orders.ToList().Count + 1,
                        del_country = "Россия",
                        del_city = "Подольск",
                        del_street = "Ленина",
                        del_house = "13",
                        del_office_or_appartment = "8",
                        del_date = DateTime.Now,
                        client_id = 1
                    };
                    db.Orders.Add(order);

                    for (int i = 0; i < productsInBasket.Count; i++) 
                    {
                        Acquisitions ac = new Acquisitions()
                        {
                            process_id = db.Acquisitions.ToList()
                            .Count + i + 1,
                            order_id = order.order_id,
                            product_id = productsInBasket[i].product_id,
                            number_of_items = productsInBasket[i].number
                        };
                        db.Acquisitions.Add(ac);
                    }

                    var wd = new WordConverter(productsInBasket, order.order_id, order.del_date.ToString());

                    try 
                    { 
                        wd.CreateDocument(); 
                    }
                    catch(Exception ex)
                    {
                        Debug.WriteLine(ex.Message);
                    }

                    productsInBasket = new List<Products>();

                    db.SaveChanges();
                    updateProducts();
                }
            }
        }
    }
}
