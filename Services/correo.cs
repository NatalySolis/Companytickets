using Microsoft.Data.SqlClient;

namespace ProyectoSistemaTickets.Services
{
    public class correo
    {
        private IConfiguration _configuration;

        public correo(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void enviar(string destinatario, string asunto, string cuerpo)
        {
            try
            {
                string connectionString = _configuration.GetSection("ConnectionStrings").GetSection("TicketConnection").Value;

                string sqlQuery = "exec msdb.dbo.sp_send_dbmail" +
                                    " @profile_name = 'Sqlmail_DAW'," +
                                    " @recipients = @par_destinatarios," +
                                    " @subject = @par_asunto," +
                                    " @body = @par_cuerpo";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        command.Parameters.AddWithValue("@par_destinatarios", destinatario);
                        command.Parameters.AddWithValue("@par_asunto", asunto);
                        command.Parameters.AddWithValue("@par_cuerpo", cuerpo);

                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al enviar correo");
                Console.WriteLine(ex.Message);
            }
        }
    }
}
