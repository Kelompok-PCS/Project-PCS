﻿using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Project.Master_menu
{
    /// <summary>
    /// Interaction logic for Insert_menu_UC.xaml
    /// </summary>
    public partial class Insert_menu_UC : UserControl
    {
        OracleConnection connection;
        Canvas canvas;
        public Insert_menu_UC(Canvas canvas)
        {
            InitializeComponent();
            connection = App.Connection;
            this.canvas = canvas;
        }
        
        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (tbNama.Text != "" && tbHarga.Text != "" && cmbKat.SelectedIndex != -1)
            {
                if (tbNama.Text != "" && tbHarga.Text != "" && cmbKat.SelectedIndex != -1)
                {
                    try
                    {
                        int harga = Convert.ToInt32(tbHarga.Text);
                        connection.Open();
                        OracleTransaction trans = connection.BeginTransaction();
                        try
                        {
                            string kode = "MEN";
                            string query =
                                "SELECT LPAD(NVL(MAX(SUBSTR(id_menu,-3,3)),0)+1,3,0) " +
                                "FROM menu " +
                                $"WHERE id_menu LIKE '{kode}%'";
                            OracleCommand cmd = new OracleCommand(query, connection);
                            kode += cmd.ExecuteScalar();
                            query =
                                $"INSERT INTO menu VALUES ('{kode}','{tbNama.Text}',{harga},'{"temp gambar"}','{tbDesc.Text}','{cmbKat.SelectedValue}','1')";
                            cmd = new OracleCommand(query, connection);
                            cmd.ExecuteNonQuery();

                            trans.Commit();
                            connection.Close();
                            MessageBox.Show("Berhasil Masukan Menu");
                        }
                        catch (Exception ex)
                        {
                            trans.Rollback();
                            connection.Close();
                            MessageBox.Show(ex.Message);
                            MessageBox.Show("Gagal Masukan Menu");
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Harga tidak valid");
                    }

                }
                else
                {
                    MessageBox.Show("Data belum lengkap");
                }
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Apakah Anda Yakin Update Menu ini ?", "Konfirmasi", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    int harga = Convert.ToInt32(tbHarga.Text);
                    connection.Open();
                    OracleTransaction trans = connection.BeginTransaction();
                    try
                    {
                        string kode = "MEN";
                        string query =
                            "SELECT LPAD(NVL(MAX(SUBSTR(id_menu,-3,3)),0)+1,3,0) " +
                            "FROM menu " +
                            $"WHERE id_menu LIKE '{kode}%'";
                        OracleCommand cmd = new OracleCommand(query, connection);
                        kode += cmd.ExecuteScalar();
                        query =
                            $"INSERT INTO menu VALUES ('{kode}','{tbNama.Text}','{tbHarga.Text}','{"temp gambar"}','{tbDesc.Text}','{cmbKat.SelectedValue}','1')";
                        cmd = new OracleCommand(query, connection);
                        cmd.ExecuteNonQuery();

                        trans.Commit();
                        connection.Close();
                        MessageBox.Show("Berhasil Masukan Menu");
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        connection.Close();
                        MessageBox.Show(ex.Message);
                        MessageBox.Show("Gagal Masukan Menu");
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Harga tidak valid");
                }

            }
            else
            {
                MessageBox.Show("Data belum lengkap");
            }
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            fillKategori();
            cmbKat.SelectedIndex = 0;
        }

        private class Kategori
        {
            public string kode { get; set; }
            public string nama { get; set; }
        }
        List<Kategori> kategoris = new List<Kategori>();
        private void fillKategori()
        {
            try
            {
                connection.Open();
                string query =
                    "SELECT ID_KATEGORI,NAMA_KATEGORI " +
                    "FROM kategori ";
                OracleCommand cmd = new OracleCommand(query, connection);
                OracleDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    kategoris.Add(new Kategori()
                    {
                        kode = reader.GetString(0),
                        nama = reader.GetString(0) + " - " + reader.GetString(1)
                    });
                }
                cmbKat.ItemsSource = kategoris;
                cmbKat.DisplayMemberPath = "nama";
                cmbKat.SelectedValuePath = "kode";
                connection.Close();
            }
            catch (Exception ex)
            {
                connection.Close();
                MessageBox.Show(ex.Message);
                MessageBox.Show("ada yang salah dengan kategori");
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            canvas.Children.Clear();
            Menu_makanan_UC menu_makanan = new Menu_makanan_UC(canvas);
            canvas.Children.Add(menu_makanan);
        }
    }
}
