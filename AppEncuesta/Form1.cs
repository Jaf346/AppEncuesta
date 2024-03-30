using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppEncuesta
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string conexion = "Data Source=DESKTOP-OHI80SJ\\SQLEXPRESS;Initial Catalog=Encuesta;Integrated Security=True;";

        private void button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection cn = new SqlConnection(conexion))
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO Encuestas (Nombre, Apellido, FechaNacimiento, Edad, CorreoElectronico, CarroPropio) values (@Nombre, @Apellido, @FechaNacimiento, @Edad, @CorreoElectronico, @CarroPropio)", cn);

                cmd.Parameters.AddWithValue("@Nombre", txtnombre.Text);
                cmd.Parameters.AddWithValue("@Apellido", txtapellido.Text);

                
                DateTime fechaNacimiento;
                if (DateTime.TryParse(txtfecha.Text, out fechaNacimiento))
                {
                    cmd.Parameters.AddWithValue("@FechaNacimiento", fechaNacimiento);
                }
                else
                {
                    
                    fechaNacimiento = DateTime.MinValue;
                    cmd.Parameters.AddWithValue("@FechaNacimiento", fechaNacimiento);
                }

                int edad;
                if (int.TryParse(txtedad.Text, out edad))
                {
                    cmd.Parameters.AddWithValue("@Edad", edad);
                }
                else
                {
                    
                    cmd.Parameters.AddWithValue("@Edad", DBNull.Value);
                }

                cmd.Parameters.AddWithValue("@CorreoElectronico", txtcorreo.Text);
                cmd.Parameters.AddWithValue("@CarroPropio", txtcarro.Text);

                cmd.CommandType = CommandType.Text;

                cn.Open();
                cmd.ExecuteNonQuery();
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            this.encuestasTableAdapter.Fill(this.encuestaDataSet.Encuestas);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(conexion))
                {
                    SqlCommand cmd = new SqlCommand("UPDATE Encuestas SET Nombre = @Nombre, Apellido = @Apellido, FechaNacimiento = @FechaNacimiento, Edad = @Edad, CorreoElectronico = @CorreoElectronico, CarroPropio = @CarroPropio WHERE IdEncuesta = @IdEncuesta", cn);

                    cmd.Parameters.AddWithValue("@Nombre", txtnombre.Text);
                    cmd.Parameters.AddWithValue("@Apellido", txtapellido.Text);

                    
                    DateTime fechaNacimiento;
                    if (DateTime.TryParse(txtfecha.Text, out fechaNacimiento))
                    {
                        cmd.Parameters.AddWithValue("@FechaNacimiento", fechaNacimiento);
                    }
                    else
                    {
                        fechaNacimiento = DateTime.MinValue;
                        cmd.Parameters.AddWithValue("@FechaNacimiento", fechaNacimiento);
                    }

                    int edad;
                    if (int.TryParse(txtedad.Text, out edad))
                    {
                        cmd.Parameters.AddWithValue("@Edad", edad);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Edad", DBNull.Value);
                    }

                    cmd.Parameters.AddWithValue("@CorreoElectronico", txtcorreo.Text);
                    cmd.Parameters.AddWithValue("@CarroPropio", txtcarro.Text);
                    cmd.Parameters.AddWithValue("@IdEncuesta", txtIdEncuesta.Text); 

                    cmd.CommandType = CommandType.Text;

                    cn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void txtEncuesta_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(conexion))
                {
                    SqlCommand cmd = new SqlCommand("DELETE FROM Encuestas WHERE IdEncuesta = @IdEncuesta", cn);

                    
                    if (!string.IsNullOrEmpty(txtIdEncuesta.Text))
                    {
                        cmd.Parameters.AddWithValue("@IdEncuesta", txtIdEncuesta.Text);
                    }
                    else
                    {
                        MessageBox.Show("Por favor, ingrese un IdEncuesta válido.");
                        return;
                    }

                    cmd.CommandType = CommandType.Text;

                    cn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Registro eliminado exitosamente.");
                    }
                    else
                    {
                        MessageBox.Show("No se encontró ningún registro con el IdEncuesta proporcionado.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }

        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                
                this.encuestasTableAdapter.Fill(this.encuestaDataSet.Encuestas);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar el DataGridView: " + ex.Message);
            }

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
