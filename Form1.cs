using Npgsql;

namespace DepersonalizationLR
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        private void btnDepersonalize_Click(object sender, EventArgs e)
        {
            DepersonalizeClass depersonalizer = new DepersonalizeClass();
            try
            {
                depersonalizer.Depersonalize();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }

        private void btnPersonalize_Click(object sender, EventArgs e)
        {
            PersonalizeClass personalizer = new PersonalizeClass();
            try
            {
                personalizer.Personalize();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message);
            }
        }
    }
}