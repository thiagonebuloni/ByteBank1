using ByteBank1.Model;
using ByteBank1.View;

namespace ByteBank1 {
    
    
    public class Program {

        

        

        private static void RegistrarNovoUsuario(List<string> cpfs, List<string> titulares, List<string> senhas, List<double> saldos, List<string> chavesPIX)
        {
            do {
                Console.Write("Digite o cpf: ");
                if (Conta.ValidaCpf())
                {
                    Interface.WriteColor("CPF inválido!", ConsoleColor.Red);
                    Console.WriteLine("\nPressione qualquer tecla para continuar");
                    Console.ReadKey();
                }  
                
                string cpf = Console.ReadLine()!;
                if (cpf.Length > 11) {
                    Interface.WriteColor("Digite o CPF sem \"-\" ou \".\".", ConsoleColor.Red);
                    Console.WriteLine("\nPressione qualquer tecla para continuar");
                    Console.ReadKey();
                }
                else if (cpf.Length < 11){
                    Interface.WriteColor("Digite o CPF com 11 dígitos.", ConsoleColor.Red);
                    Console.WriteLine("\nPressione qualquer tecla para continuar");
                    Console.ReadKey();
                }
                else {
                    cpfs.Add(cpf);
                    break;
                }
            }
            while (true);
            
            do {
                Console.Write("Digite o nome: ");
                string nome = Console.ReadLine()!;
                if (nome == "") {
                    Interface.WriteColor("Digite um nome inválido.", ConsoleColor.Red);
                    Console.WriteLine("\nPressione qualquer tecla para continuar");
                    Console.ReadKey();
                }
                else {
                    titulares.Add(nome);
                    break;
                }
            }
            while (true);

            do {
                Console.Write("Insira uma senha: ");
                string senha = Conta.GetPass();
                if (senha.Length < 8) {
                    Interface.WriteColor("Insira uma senha com pelo menos 8 caracteres.", ConsoleColor.Red);
                    Console.WriteLine("\nPressione qualquer tecla para continuar");
                    Console.ReadKey();
                }
                else {
                    senhas.Add(senha);
                    break;
                }
            }
            while (true);

            saldos.Add(0);
            chavesPIX.Add("0");
        }

        public static void DeletarUsuario(List<string> cpfs, List<string> titulares, List<string> senhas, List<double> saldos) {
            Console.WriteLine("Digite o cpf: ");
            string cpfParaDeletar = Console.ReadLine()!;
            int indexParaDeletar = cpfs.FindIndex(cpf => cpf == cpfParaDeletar);

            if (indexParaDeletar == -1) {
                Interface.WriteColor("\nNão foi possivel deletar esta conta.\nMOTIVO: Conta não encontrada.", ConsoleColor.Red);
                Console.WriteLine("\nPressione qualquer tecla para continuar...");
                Console.ReadKey();
            }
            else {

                cpfs.Remove(cpfParaDeletar);
                titulares.RemoveAt(indexParaDeletar);
                senhas.RemoveAt(indexParaDeletar);
                saldos.RemoveAt(indexParaDeletar);

                Interface.WriteColor("\nConta deletada com sucesso.", ConsoleColor.Green);
                Console.WriteLine("\nPressione qualquer tecla para continuar...");
                Console.ReadKey();
            }
        }

        public static void ListarTodasAsContas(List<string> cpfs, List<string> titulares, List<double> saldos)
        {
            Console.WriteLine();
            
            if (cpfs.Count() != 0) {
                for (int i = 0; i < cpfs.Count; i++) {
                    Console.WriteLine($"CPF = {cpfs[i]} \t|\t Titular = {titulares[i]}\t |\t Saldo = R$ {saldos[i]}");
                }
            }
            else {
                Interface.WriteColor("Não há contas registradas atualmente.", ConsoleColor.Red);
            }
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }


        
        


        

        public static void Main(string[] args) {

            List<string> cpfs = new List<string>();
            List<string> titulares = new List<string>();
            List<string> senhas = new List<string>();
            List<double> saldos = new List<double>();
            List<string> chavesPIX = new List<string>();

            int indexParaPesquisar = 0;
            bool logged = false;

            int option = 0;

            do {
                Interface.showMenu();
                
                try {
                    option = int.Parse(Console.ReadLine()!);
                }
                catch (Exception) {
                    option = 10;
                }
                
                switch (option) {
                    case 0:
                        Console.WriteLine("O programa está sendo encerrado...");
                        break;
                    case 1:
                        RegistrarNovoUsuario(cpfs, titulares, senhas, saldos, chavesPIX);
                        break;
                    case 2:
                        DeletarUsuario(cpfs, titulares, senhas, saldos);
                        break;
                    case 3:
                        ListarTodasAsContas(cpfs, titulares, saldos);
                        break;
                    case 4:
                        Interface.ApresentarUsuario(cpfs, titulares, saldos);
                        break;
                    case 5:
                        Interface.QuantiaArmazenada(cpfs, titulares, saldos);
                        break;
                    case 6:
                        do {
                            if (!logged) {
                                if (logged = Conta.Login(cpfs, senhas, logged, indexParaPesquisar)) {
                                    Interface.ManipularConta(cpfs, titulares, saldos, senhas, chavesPIX, logged, indexParaPesquisar);
                                }
                                else {
                                    logged = false;
                                    option = 10;
                                    continue;
                                }
                            }
                            else Interface.ManipularConta(cpfs, titulares, saldos, senhas, chavesPIX, logged, indexParaPesquisar);

                            try {
                                option = int.Parse(Console.ReadLine()!);
                            }
                            catch (Exception) {
                                option = 10;
                            }
                            
                            switch (option) {
                                case 0:
                                    option = 9;
                                    break;
                                case 1:
                                    Conta.FazerDeposito(cpfs, titulares, saldos, logged, indexParaPesquisar);
                                    break;
                                case 2:
                                    Conta.FazerSaque(cpfs, titulares, saldos, senhas, logged, indexParaPesquisar);
                                    break;
                                case 3:
                                    Conta.FazerPIX(cpfs, titulares, saldos, senhas, chavesPIX, logged, indexParaPesquisar);
                                    break;
                                case 4:
                                    Conta.GerarChavePIX(cpfs, titulares, saldos, senhas, chavesPIX, logged, indexParaPesquisar);
                                    break;
                                case 5:
                                    logged = Conta.Logout(logged);
                                    option = 9;
                                    break;
                            }
                        } while(option != 9);
                    break;
                }
            } while(option != 0);
        }

        
    }
}