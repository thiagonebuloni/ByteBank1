namespace byteBank1 {

    
    
    public class Program {

        public static string GetPass() {
            string pass = string.Empty;
            ConsoleKey key;

            do
            {
                var keyInfo = Console.ReadKey(intercept: true);
                key = keyInfo.Key;

                if (key == ConsoleKey.Backspace && pass.Length > 0)
                {
                    Console.Write("\b \b");
                    pass = pass[0..^1];
                }
                else if (!char.IsControl(keyInfo.KeyChar))
                {
                    Console.Write("*");
                    pass += keyInfo.KeyChar;
                }
            } while (key != ConsoleKey.Enter);
            
            Console.WriteLine();
            return pass;
        }

        public static void showMenu() {
            Console.WriteLine("\n----------------------------------------");
            Console.WriteLine("1 - Inserir novo usuário");
            Console.WriteLine("2 - Deletar um usuário");
            Console.WriteLine("3 - Listar todas as contas registradas");
            Console.WriteLine("4 - Detalhes de um usuário");
            Console.WriteLine("5 - Quantia armazenada no banco");
            Console.WriteLine("6 - Manipular a conta");
            Console.WriteLine("0 - Para sair do programa");
            Console.WriteLine("----------------------------------------");
            Console.Write("\nDigite a opção desejada: ");
        }

        private static void RegistrarNovoUsuario(List<string> cpfs, List<string> titulares, List<string> senhas, List<double> saldos)
        {
            Console.Write("Digite o cpf: ");
            cpfs.Add(Console.ReadLine());
            Console.Write("Digite o nome: ");
            titulares.Add(Console.ReadLine());
            Console.Write("Insira a senha: ");
            senhas.Add(GetPass());
            saldos.Add(0);
        }

        public static void DeletarUsuario(List<string> cpfs, List<string> titulares, List<string> senhas, List<double> saldos) {
            Console.WriteLine("Digite o cpf: ");
            string cpfParaDeletar = Console.ReadLine();
            int indexParaDeletar = cpfs.FindIndex(cpf => cpf == cpfParaDeletar);

            if (indexParaDeletar == -1) {
                WriteColor("\nNão foi possivel deletar esta conta.\nMOTIVO: Conta não encontrada.", "Red");
                Console.WriteLine("\nPressione qualquer tecla para continuar...");
                Console.ReadKey();
            }
            else {

                cpfs.Remove(cpfParaDeletar);
                titulares.RemoveAt(indexParaDeletar);
                senhas.RemoveAt(indexParaDeletar);
                saldos.RemoveAt(indexParaDeletar);

                WriteColor("\nConta deletada com sucesso.", "Green");
                Console.WriteLine("\nPressione qualquer tecla para continuar...");
                Console.ReadKey();
            }
        }

        public static void ListarTodasAsContas(List<string> cpfs, List<string> titulares, List<double> saldos)
        {
            Console.WriteLine();
            
            if (cpfs.Count() != 0) {
                for (int i = 0; i < cpfs.Count; i++) {
                    Console.WriteLine($"CPF = {cpfs[i]} \t|\t Titular = {titulares[i]}\t |\t Saldo = {saldos[i]}");
                }
            }
            else {
                WriteColor("Não há contas registradas atualmente.", "Red");
            }
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }

        public static void Soma(List<double> saldos) {
            // return saldos.Aggregate(0.0, (x, y) => x + y); // Func / Delegate
            // return saldos.Sum();
            Console.WriteLine($"Total acumulado no banco: {saldos.Sum()}");
        }

        public static void ApresentarUsuario(List<string> cpfs, List<string> titulares, List<double> saldos) {
            
            Console.Write("Digite o cpf: ");
            string cpfParaApresentar = Console.ReadLine();
            int indexParaPesquisar = cpfs.FindIndex(cpf => cpf == cpfParaApresentar);

            if (indexParaPesquisar == -1) {
                WriteColor("\nNão foi possivel apresentar esta conta.\nMOTIVO: Conta não encontrada.", "Red");
            }
            else {
                Console.WriteLine($"\nCPF = {cpfs[indexParaPesquisar]} \t|\t Titular = {titulares[indexParaPesquisar]}\t |\t Saldo = {saldos[indexParaPesquisar]}");
                Console.WriteLine("\nPressione qualquer tecla para continuar...");
                Console.ReadKey();
            }
        }

        public static void apresentaConta( int index, List<string> cpfs, List<string> titulares, List<double> saldos) {
            Console.WriteLine($"CPF = {cpfs[index]} | Titular = {titulares[index]} | Saldo = {saldos[index]}");
        }

        public static void QuantiaArmazenada(List<string> cpfs, List<string> titulares, List<double> saldos)
        {
            Console.WriteLine();
            Soma(saldos);
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }

        public static void ManipularConta(List<string> cpfs, List<string> titulares, List<double> saldos) {
            Console.WriteLine("\n----------------------------------------");
            Console.WriteLine("1 - Fazer um depósito");
            Console.WriteLine("2 - Fazer um saque");
            Console.WriteLine("9 - Voltar para o menu anterior");
            Console.WriteLine("----------------------------------------");
            Console.Write("\nDigite a opção desejada: ");

        }

        public static void WriteColor(string txt, string color) {
            
            if (color == "Red" || color == "red") {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(txt);
                Console.ResetColor();
            }
            else if (color == "Green" || color == "green") {
                Console.ForegroundColor = ConsoleColor.Green;
                System.Console.WriteLine(txt);
                Console.ResetColor();
            }
        }

        public static void FazerSaque(List<string> cpfs, List<string> titulares, List<double> saldos, List<string> senhas)
        {
            Console.Write("\nDigite o seu cpf:");
            string cpfParaApresentar = Console.ReadLine();
            int indexParaPesquisar = cpfs.FindIndex(cpf => cpf == cpfParaApresentar);

            if (indexParaPesquisar == -1) {
                WriteColor("\nConta não encontrada.\nConfira o cpf e tente novamente.", "Red");
            }
            else {
                Console.Write("Digite sua senha: ");
                string senhaParaApresentar = GetPass();
                int indexSenhaParaPesquisar = senhas.FindIndex(senha => senha == senhaParaApresentar);
            
                int contSenhaErrada = 3;
                if (indexSenhaParaPesquisar == -1) {
                    contSenhaErrada -= 1;
                    WriteColor($"\nSenha incorreta. Tente novamente\nRestam {contSenhaErrada} tentativas.", "Red");
                }
                else {
                    Console.Write("Digite a quantia a ser sacada no formato: 0000,00: ");
                    double valorSaque = double.Parse(Console.ReadLine());
                    double valorSaldoAtualizado = saldos[indexSenhaParaPesquisar] - valorSaque;

                    if (valorSaldoAtualizado >= 0) {
                        System.Console.WriteLine($"valorSaldoAtualizado = {valorSaldoAtualizado}");
                        saldos[indexParaPesquisar] = valorSaldoAtualizado;
                        WriteColor("\nSaque efetuado.", "Green");
                    }
                    else {
                        System.Console.WriteLine($"valorSaldoAtualizado = {valorSaldoAtualizado}");
                        WriteColor($"\nSaldo insuficiente.\nSeu saldo atual é {saldos[indexParaPesquisar]:F2}", "Red");
                    }
                    
                    Console.WriteLine("\nPressione qualquer tecla para continuar...");
                    Console.ReadKey();
                }
            }
        }

        public static void FazerDeposito(List<string> cpfs, List<string> titulares, List<double> saldos)
        {
            Console.Write("\nDigite o cpf do titular:");
            string cpfParaApresentar = Console.ReadLine();
            int indexParaPesquisar = cpfs.FindIndex(cpf => cpf == cpfParaApresentar);

            if (indexParaPesquisar == -1) {
                WriteColor("\nConta não encontrada.\nConfira o cpf e tente novamente.", "Red");
            }
            else {
                Console.Write("\nDigite a quantia a ser depositada no formato: 0000,00: ");
                saldos[indexParaPesquisar] += double.Parse(Console.ReadLine());
                
                WriteColor("\nDepósito efetuado.", "Green");

                Console.WriteLine("\nPressione qualquer tecla para continuar...");
                Console.ReadKey();
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
                        Console.WriteLine("O programa está sendo encerrado...");
                        break;
                    case 1:
                        RegistrarNovoUsuario(cpfs, titulares, senhas, saldos);
                        break;
                    case 2:
                        DeletarUsuario(cpfs, titulares, senhas, saldos);
                        break;
                    case 3:
                        ListarTodasAsContas(cpfs, titulares, saldos);
                        break;
                    case 4:
                        ApresentarUsuario(cpfs, titulares, saldos);
                        break;
                    case 5:
                        QuantiaArmazenada(cpfs, titulares, saldos);
                        break;
                    case 6:
                        do {
                            ManipularConta(cpfs, titulares, saldos);

                            option = int.Parse(Console.ReadLine());
                            
                            switch (option) {
                                case 1:
                                    FazerDeposito(cpfs, titulares, saldos);
                                    break;
                                case 2:
                                    FazerSaque(cpfs, titulares, saldos, senhas);
                                    break;
                                case 9:
                                    break;
                            }
                        } while(option != 9);
                        break;
                }
            } while(option != 0);
        }

    }
}