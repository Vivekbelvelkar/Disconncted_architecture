using System;
using System.Data;
using System.Data.SqlClient;

namespace webapp_disconnected150225
{
    public partial class disconnected : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btndelete_Click(object sender, EventArgs e)
        {
            string cs = "Data Source=VIVEK-03\\SQLEXPRESS01;Initial Catalog=practdb;Integrated Security=True;TrustServerCertificate=True";

            SqlConnection conn = new SqlConnection(cs);
            SqlDataAdapter ADP = new SqlDataAdapter("SELECT * FROM employee", conn);
            SqlCommandBuilder cmb = new SqlCommandBuilder(ADP);

            DataSet ds = new DataSet();
            ADP.Fill(ds,"employee");

            bool isDeleted = false; 
            string nm = txtname.Text;
            string locate = txtlocation.Text;
            
            foreach (DataRow row in ds.Tables["employee"].Rows)
            {

                if (!String.IsNullOrEmpty(nm)  &&   row["location"].ToString().Trim() == txtlocation.Text)
                {
                    row.Delete();
                    isDeleted = true;
                    // break;
                }
                if (!String.IsNullOrEmpty(locate)   && row["location"].ToString().Trim() == txtlocation.Text)
                {
                    row.Delete();
                    isDeleted = true;
                    break;
                }
            }
            ADP.Update(ds,"employee");
            if (isDeleted)
            {
                Response.Write("<script>alert('Record deleted successfully!');</script>");
            }
            else
            {
                Response.Write("<script>alert('No matching record found.');</script>");
            }
           
        }



        protected void btninsert_Click(object sender, EventArgs e)
        {
            string cs = "Data Source=VIVEK-03\\SQLEXPRESS01;Initial Catalog=practdb;Integrated Security=True;TrustServerCertificate=True";

            SqlDataAdapter ADP = new SqlDataAdapter("Select * from employee", cs);
            SqlCommandBuilder cmb = new SqlCommandBuilder(ADP);

            DataSet ds = new DataSet();
            ADP.Fill(ds, "employee");

            DataRow row = ds.Tables["employee"].NewRow();


            string name = txtname.Text;
            int salary = int.Parse(txtsalary.Text);
            string address = txtlocation.Text;


            row[1] = name;
            row[2] = salary;
            row[3] = address;

            ds.Tables["employee"].Rows.Add(row);

            int n = ADP.Update(ds, "employee");
            if (n != 0)
            {
                Response.Write("<script>alert('record inserted successfully')</script>");
            }
            else
            {
                Response.Write("<script>alert('record not inserted')</script>");

            }

        }
    }
}