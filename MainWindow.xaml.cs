using System;
using System.Windows;
using System.Windows.Controls;
using Crud_Personne;

namespace Crud_Personne
{
    public partial class MainWindow : Window
    {
        private readonly Database db = new Database();

        public MainWindow()
        {
            InitializeComponent();
            ChargerPersonnes();
        }


        private void ChargerPersonnes()
        {
            dataGridPersonnes.Columns.Clear();
            dataGridPersonnes.ItemsSource = db.GetPersonnes();
            AjouterColonnesBoutons();
        }

        private void btnEnregistrer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var p = new Personne
                {
                    Nom = txtNom.Text,
                    Prenoms = txtPrenoms.Text,
                    Age = int.Parse(txtAge.Text),
                    NumeroTel = txtTel.Text
                };

                if (btnEnregistrer.Tag == null)
                {
                    db.AjouterPersonne(p);
                    MessageBox.Show("Ajout réussi !");
                }
                else
                {
                    p.Id = (int)btnEnregistrer.Tag;
                    db.ModifierPersonne(p);
                    MessageBox.Show("Mise à jour réussie !");
                    btnEnregistrer.Content = "Enregistrer";
                    btnEnregistrer.Tag = null;
                }

                ClearForm();
                ChargerPersonnes();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur : " + ex.Message);
            }
        }

        private void ClearForm()
        {
            txtNom.Clear();
            txtPrenoms.Clear();
            txtAge.Clear();
            txtTel.Clear();
        }

        private void AjouterColonnesBoutons()
        {
            // Bouton Modifier
            DataGridTemplateColumn colModif = new DataGridTemplateColumn();
            colModif.Header = "Modifier";
            DataTemplate btnModifTemplate = new DataTemplate();

            FrameworkElementFactory btnModif = new FrameworkElementFactory(typeof(Button));
            btnModif.SetValue(Button.ContentProperty, "✎");
            btnModif.AddHandler(Button.ClickEvent, new RoutedEventHandler(BtnModifier_Click));
            btnModifTemplate.VisualTree = btnModif;
            colModif.CellTemplate = btnModifTemplate;
            dataGridPersonnes.Columns.Add(colModif);

            // Bouton Supprimer
            DataGridTemplateColumn colSupp = new DataGridTemplateColumn();
            colSupp.Header = "Supprimer";
            DataTemplate btnSuppTemplate = new DataTemplate();

            FrameworkElementFactory btnSupp = new FrameworkElementFactory(typeof(Button));
            btnSupp.SetValue(Button.ContentProperty, "🗑");
            btnSupp.AddHandler(Button.ClickEvent, new RoutedEventHandler(BtnSupprimer_Click));
            btnSuppTemplate.VisualTree = btnSupp;
            colSupp.CellTemplate = btnSuppTemplate;
            dataGridPersonnes.Columns.Add(colSupp);
        }

        private void BtnModifier_Click(object sender, RoutedEventArgs e)
        {
            var p = (sender as Button).DataContext as Personne;

            if (p != null)
            {
                txtNom.Text = p.Nom;
                txtPrenoms.Text = p.Prenoms;
                txtAge.Text = p.Age.ToString();
                txtTel.Text = p.NumeroTel;

                btnEnregistrer.Content = "Mettre à jour";
                btnEnregistrer.Tag = p.Id;
            }
        }

        private void BtnSupprimer_Click(object sender, RoutedEventArgs e)
        {
            var p = (sender as Button).DataContext as Personne;

            if (p != null && MessageBox.Show("Confirmer la suppression ?", "Supprimer", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                db.SupprimerPersonne(p.Id);
                ChargerPersonnes();
            }
        }

        
    }
}
