using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Drawing;
namespace _23521005_APIExtracting
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox1.BackColor = Color.Gold;
            textBox1.ForeColor = Color.Black;

            textBox1.Text = "Loading...";

            var client = new HttpClient();
            string domain = url.Text.Trim();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://whois-api6.p.rapidapi.com/whois/api/v1/getData"),
                Headers =
                {
                    { "x-rapidapi-key", "21bcf62944msh6aa96adf63d6582p1ddb6fjsn0d818f5e8019" },
                    { "x-rapidapi-host", "whois-api6.p.rapidapi.com" },
                },
                Content = new StringContent($"{{\"query\":\"{domain}\"}}", Encoding.UTF8, "application/json")
            };

            try
            {
                using (var response = await client.SendAsync(request))
                {
                    //response.EnsureSuccessStatusCode();
                    //var body = await response.Content.ReadAsStringAsync();
                    response.EnsureSuccessStatusCode();
                    var statusCode = (int)response.StatusCode;

                    var body = await response.Content.ReadAsStringAsync();

                    string stt = "";
                    clear_all();


                        if ((int)response.StatusCode >= 200 && (int)response.StatusCode < 300) { stt = "Successfully!"; textBox1.BackColor = Color.LimeGreen; textBox1.ForeColor = Color.White; }
                    else { stt = "ERROR"; textBox1.BackColor = Color.Red; }
                    textBox1.Text = $"{stt}";



                    JObject json = JObject.Parse(body);
                    var result = json["result"];

                    //If all null
                    bool isAllNull = result == null || !result.Children<JProperty>().Any(p =>
                        p.Value.Type != JTokenType.Null &&
                        !(p.Value.Type == JTokenType.Array && !p.Value.HasValues) &&
                        !(p.Value.Type == JTokenType.String && string.IsNullOrWhiteSpace(p.Value.ToString()))
                    );

                    if (isAllNull)
                    {
                        textBox1.Text = "Domain Information not found!";
                        textBox1.ForeColor = Color.White;
                        textBox1.BackColor = Color.Red;
                        return;
                    }


                    string updatedDates = string.Join(", ", result["updated_date"] ?? new JArray());
                    string creationDates = string.Join(", ", result["creation_date"] ?? new JArray());
                    string expirationDates = string.Join(", ", result["expiration_date"] ?? new JArray());
                    string nameServers = string.Join(", \n ", result["name_servers"] ?? new JArray());
                    string status = string.Join("", result["status"] ?? new JArray());
                    string emails = string.Join(", ", result["emails"] ?? new JArray());


                    string country = result["country"]?.ToString() ?? "";
                    string state = result["state"]?.ToString() ?? "";
                    string city = result["city"]?.ToString() ?? "";
                    string local = string.Join(" , ", new[] { country, state, city }.Where(x => !string.IsNullOrWhiteSpace(x)));

                    DomainName_text.Text = string.IsNullOrWhiteSpace(result["domain_name"]?.ToString()) ? " N/A" : result["domain_name"].ToString();
                    DomainName_text.Text += string.IsNullOrWhiteSpace(result["org"]?.ToString()) ? " - Organization: N/A" : $" - {result["org"]}".ToString();
                    
                    text_reg.Text = string.IsNullOrWhiteSpace(result["registrar"]?.ToString()) ? " N/A" : $" {result["registrar"]}";
                    var regUrlArray = result["registrar_url"] as JArray;
                    text_regurl.Text = (regUrlArray != null && regUrlArray.Any())
                        ? string.Join(",\n", regUrlArray.Select(url => url.ToString()).Where(url => !string.IsNullOrWhiteSpace(url)))
                        : " N/A";
                    text_reseller.Text = string.IsNullOrWhiteSpace(result["reseller"]?.ToString()) ? " N/A" : $" {result["reseller"]}".ToString();
                    whois_text.Text = string.IsNullOrWhiteSpace(result["whois_server"]?.ToString()) ? " N/A" : $" {result["whois_server"]}".ToString();
                    email.Text = string.IsNullOrWhiteSpace(emails) ? " N/A" : $"{emails}\n";

                    text_area.Text= string.IsNullOrWhiteSpace($"{local}"?.ToString()) ? " N/A" : $" {local}";
                    text_add.Text = string.IsNullOrWhiteSpace(result["address"]?.ToString()) ? " N/A" : $" {result["address"]}";
                    text_postal.Text=string.IsNullOrWhiteSpace(result["registrant_postal_code"]?.ToString()) ? " N/A" : $" {result["registrant_postal_code"]}";
                    
                    
                    text_updated.Text = string.IsNullOrWhiteSpace(emails) ? "N/A" : $"{updatedDates}\n";
                    text_cre.Text = string.IsNullOrWhiteSpace(creationDates) ? "N/A" : $"{creationDates}\n";
                    text_ex.Text = string.IsNullOrWhiteSpace(expirationDates) ? "N/A" : $"{expirationDates}\n";


                    text_server.Text = string.IsNullOrWhiteSpace(nameServers) ? " N/A" : $" {nameServers}\n";
                    text_status.Text = string.IsNullOrWhiteSpace(status) ? " N/A" : $"{status}\n";





                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}");
            }
        }
        private void clear_all()
        {
            DomainName_text.Text = " N/A";
            text_reg.Text = " N/A";
            text_regurl.Text = " N/A";
            text_reseller.Text = " N/A";
            whois_text.Text = " N/A";
            email.Text = " N/A";
            text_area.Text =  " N/A";
            text_add.Text = " N/A";
            text_postal.Text = " N/A";
            text_updated.Text = " N/A";
            text_cre.Text = " N/A";
            text_ex.Text = " N/A";
            text_server.Text = " N/A";
            text_status.Text = " N/A";
        }


        

        
    }
}


