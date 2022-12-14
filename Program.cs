namespace byteBank1 {

    public class Program {

        static void showMenu() {
            System.Console.WriteLine("1 - Inserir novo usuário");
            System.Console.WriteLine("2 - Deletar um usuário");
            System.Console.WriteLine("3 - Listar todas as contas registradas");
            System.Console.WriteLine("4 - Detalhes de um usuário");
            System.Console.WriteLine("5 - Quantia armazenada no banco");
            System.Console.WriteLine("6 - Manipular a conta");
            System.Console.WriteLine("0 - Para sair do programa");
            System.Console.Write("Digite a opção desejada: ");
        }

        private static void RegistrarNovoUsuario(List<string> cpfs, List<string> titulares, List<string> senhas, List<double> saldos)
        {
            Console.Write("Digite o cpf: ");
            cpfs.Add(Console.ReadLine());
            Console.Write("Digite o nome: ");
            titulares.Add(Console.ReadLine());
            Console.Write("Insira a senha: ");
            senhas.Add(Console.ReadLine());
            Console.Write("Digite o saldo: ");
            saldos.Add(0);
        }

        private static void ListarTodasAsContas(List<string> cpfs, List<string> titulares, List<double> saldos)
        {
            for (int i = 0; i < cpfs.Count; i++) {
                Console.WriteLine($"CPF = {cpfs[i]} | Titular = {titulares[i]} | Saldo = {saldos[i]}");
            }
        }

        public static void Main(string[] args) {

            Console.WriteLine("Antes de começar a usar, vamos configurar alguns valores: ");
            
            Console.WriteLine("Digite a quantidade de usuários: ");
            int quantidadeDeUsuarios = int.Parse(Console.ReadLine());

            List<string> cpfs = new List<string>();
            List<string> titulares = new List<string>();
            List<string> senhas = new List<string>();
            List<double> saldos = new List<double>();

            int option;

            do {
                showMenu();
                option = int.Parse(Console.ReadLine());
                
                switch (option) {
                    case 0:
                        Console.WriteLine("Estou encerrando o programa");
                        break;
                    case 1:
                        RegistrarNovoUsuario(cpfs, titulares, senhas, saldos);
                        break;
                    case 2:
                        Console.WriteLine("Deveria estar deletando um usuário");
                        break;
                    case 3:
                        ListarTodasAsContas(cpfs, titulares, saldos);
                        break;
                    case 4:
                        Console.WriteLine("Deveria estar mostrando detalhes de um usuário");
                        break;
                    case 5:
                        Console.WriteLine();
                        break;
                    case 6:
                        Console.WriteLine();
                        break;
                }
            } while(option != 0);
        }

        
    }
}