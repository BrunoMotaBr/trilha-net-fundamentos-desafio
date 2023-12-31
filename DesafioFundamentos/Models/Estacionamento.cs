using System.Text.RegularExpressions;

namespace DesafioFundamentos.Models
{
    public class Estacionamento
    {
        private decimal precoInicial = 0;
        private decimal precoPorHora = 0;
        private List<string> veiculos = new List<string>();

        public Estacionamento(decimal precoInicial, decimal precoPorHora)
        {
            this.precoInicial = precoInicial;
            this.precoPorHora = precoPorHora;
        }

        public bool AdicionarVeiculo()
        {
            string placa = "";
            string resposta = "";
            Console.Write("Digite a placa do veículo para estacionar: ");
            placa = Console.ReadLine();
            if(ValidarPlaca(placa)){
                veiculos.Add(placa);
                return true;
            }else{
                Console.WriteLine("Placa invalida esperamos os seguintes padrões (AAA-1111) e/ou (AAA1A11)");
                Console.Write("Pressione ENTER para continuar ou 0 e ENTER para voltar: ");
                resposta = Console.ReadLine();
                Console.Clear();
                if(resposta == "0"){
                    return false;
                }else{
                    AdicionarVeiculo();
                    return false;
                }
            }
        }

        public void RemoverVeiculo()
        {
            string placa = "";
            Console.Write("Digite a placa do veículo para remover: ");
            placa = Console.ReadLine();
            // Verifica se o veículo existe
            if (veiculos.Any(x => x.ToUpper() == placa.ToUpper()))
            {
                int horas = 0;
                Console.Write("Digite a quantidade de horas que o veículo permaneceu estacionado: ");
                horas = Convert.ToInt32(Console.ReadLine()); 
                
                decimal valorTotal = CalcularValorParaReceber(horas); 

                veiculos.Remove(placa);
                Console.WriteLine($"O veículo {placa} foi removido e o preço total foi de: R$ {valorTotal}");
            }
            else
            {
                Console.WriteLine("Desculpe, esse veículo não está estacionado aqui. Confira se digitou a placa corretamente");
            }
        }

        public void ListarVeiculos()
        {
            // Verifica se há veículos no estacionamento
            if (veiculos.Any())
            {
                Console.WriteLine("Os veículos estacionados são:");
                foreach(string veiculo in veiculos){
                    Console.WriteLine(veiculo.ToUpper());
                }
            }
            else
            {
                Console.WriteLine("Não há veículos estacionados.");
            }
        }
        private bool ValidarPlaca(string placa){

            //Verifica se o usuario digitou uma placa valida seguindo um dos dois padroes (AAA-1111) ou (AAA1A11)
            string padraoAntigo = @"^[a-zA-Z]{3}-\d{4}$";
            string padraoNovo = @"^[a-zA-Z]{3}\d[a-zA-Z]\d{2}$";
            if (Regex.IsMatch(placa, padraoAntigo) || Regex.IsMatch(placa, padraoNovo)) return true;
            else return false;

        }
        private decimal CalcularValorParaReceber(decimal horas){
            // Responsavel por realizar o calculo do valor
            return this.precoInicial + (this.precoPorHora * horas);
        }
    }
}
