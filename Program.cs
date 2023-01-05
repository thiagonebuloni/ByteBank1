namespace ByteBank1 {
    
    
    public class Program {

        private static string GetPass() {
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
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("\n----------------------------------------");
            Console.WriteLine("[1] - Inserir novo usuário");
            Console.WriteLine("[2] - Deletar um usuário");
            Console.WriteLine("[3] - Listar todas as contas registradas");
            Console.WriteLine("[4] - Detalhes de um usuário");
            Console.WriteLine("[5] - Quantia armazenada no banco");
            Console.WriteLine("[6] - Operações da conta");
            Console.WriteLine("[0] - Para sair do programa");
            Console.WriteLine("----------------------------------------");
            Console.Write("\nDigite a opção desejada: ");
        }

        private static void RegistrarNovoUsuario(List<string> cpfs, List<string> titulares, List<string> senhas, List<double> saldos, List<string> chavesPIX)
        {
            do {
                Console.Write("Digite o cpf: ");
                string cpf = Console.ReadLine();
                if (cpf.Length > 11) {
                    WriteColor("Digite o CPF sem \"-\" ou \".\".", ConsoleColor.Red);
                    Console.WriteLine("\nPressione qualquer tecla para continuar");
                    Console.ReadKey();
                }
                else if (cpf.Length < 11){
                    WriteColor("Digite o CPF com 11 dígitos.", ConsoleColor.Red);
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
                string nome = Console.ReadLine();
                if (nome == "") {
                    WriteColor("Digite um nome inválido.", ConsoleColor.Red);
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
                string senha = GetPass();
                if (senha.Length < 8) {
                    WriteColor("Insira uma senha com pelo menos 8 caracteres.", ConsoleColor.Red);
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
            string cpfParaDeletar = Console.ReadLine();
            int indexParaDeletar = cpfs.FindIndex(cpf => cpf == cpfParaDeletar);

            if (indexParaDeletar == -1) {
                WriteColor("\nNão foi possivel deletar esta conta.\nMOTIVO: Conta não encontrada.", ConsoleColor.Red);
                Console.WriteLine("\nPressione qualquer tecla para continuar...");
                Console.ReadKey();
            }
            else {

                cpfs.Remove(cpfParaDeletar);
                titulares.RemoveAt(indexParaDeletar);
                senhas.RemoveAt(indexParaDeletar);
                saldos.RemoveAt(indexParaDeletar);

                WriteColor("\nConta deletada com sucesso.", ConsoleColor.Green);
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
                WriteColor("Não há contas registradas atualmente.", ConsoleColor.Red);
            }
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }

        public static void Soma(List<double> saldos) {
            Console.WriteLine($"Total acumulado no banco: {saldos.Sum()}");
        }

        public static void ApresentarUsuario(List<string> cpfs, List<string> titulares, List<double> saldos) {
            
            Console.Write("Digite o cpf: ");
            string cpfParaApresentar = Console.ReadLine();
            int indexParaPesquisar = cpfs.FindIndex(cpf => cpf == cpfParaApresentar);

            if (indexParaPesquisar == -1) {
                WriteColor("\nNão foi possivel apresentar esta conta.\nMOTIVO: Conta não encontrada.", ConsoleColor.Red);
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
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("\n----------------------------------------");
            Console.WriteLine("[1] - Fazer um depósito");
            Console.WriteLine("[2] - Fazer um saque");
            Console.WriteLine("[3] - Fazer um PIX");
            Console.WriteLine("[4] - Receber um PIX");
            Console.WriteLine("[9] - Voltar para o menu anterior");
            Console.WriteLine("----------------------------------------");
            Console.Write("\nDigite a opção desejada: ");

        }

        public static void WriteColor(string txt, ConsoleColor color) {
            
            Console.ForegroundColor = color;
            Console.WriteLine(txt);
            Console.ForegroundColor = ConsoleColor.DarkGreen;
        }
        
        public static void FazerSaque(List<string> cpfs, List<string> titulares, List<double> saldos, List<string> senhas)
        {
            Console.Write("\nDigite o seu cpf:");
            string cpfParaApresentar = Console.ReadLine();
            int indexParaPesquisar = cpfs.FindIndex(cpf => cpf == cpfParaApresentar);

            if (indexParaPesquisar == -1) {
                WriteColor("\nConta não encontrada.\nConfira o CPF e tente novamente.", ConsoleColor.Red);
                Console.WriteLine("Aperte qualquer tecla para continuar.");
                Console.ReadKey();
                Console.Clear();
            }
            else {
                Console.Write("Digite sua senha: ");
                string senhaParaApresentar = GetPass();
                int indexSenhaParaPesquisar = senhas.FindIndex(senha => senha == senhaParaApresentar);
            
                int contSenhaErrada = 3;
                if (indexSenhaParaPesquisar == -1) {
                    contSenhaErrada -= 1;
                    WriteColor($"\nSenha incorreta. Tente novamente.\nRestam {contSenhaErrada} tentativas.", ConsoleColor.Red);
                    Console.WriteLine("Aperte qualquer tecla para continuar.");
                    Console.ReadKey();
                    Console.Clear();
                }
                else {
                    Console.Write("\nDigite a quantia a ser sacada no formato: 0000,00: ");
                    double valorSaque = double.Parse(Console.ReadLine());
                    double valorSaldoAtualizado = saldos[indexSenhaParaPesquisar] - valorSaque;

                    if (valorSaldoAtualizado >= 0) {
                        System.Console.WriteLine($"valorSaldoAtualizado = {valorSaldoAtualizado}");
                        saldos[indexParaPesquisar] = valorSaldoAtualizado;
                        WriteColor("\nSaque efetuado.", ConsoleColor.Green);
                    }
                    else {
                        System.Console.WriteLine($"valorSaldoAtualizado = {valorSaldoAtualizado}");
                        WriteColor($"\nSaldo insuficiente.\nSeu saldo atual é {saldos[indexParaPesquisar]:F2}", ConsoleColor.Red);
                    }
                    
                    Console.WriteLine("\nPressione qualquer tecla para continuar...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }
        }

        public static void FazerDeposito(List<string> cpfs, List<string> titulares, List<double> saldos)
        {
            Console.Write("\nDigite o cpf do titular:");
            string cpfParaApresentar = Console.ReadLine();
            int indexParaPesquisar = cpfs.FindIndex(cpf => cpf == cpfParaApresentar);

            if (indexParaPesquisar == -1) {
                WriteColor("\nConta não encontrada.\nConfira o cpf e tente novamente.", ConsoleColor.Red);
                Console.WriteLine("Aperte qualquer tecla para continuar.");
                Console.ReadKey();
                Console.Clear();
            }
            else {
                Console.Write("\nDigite a quantia a ser depositada no formato: 0000,00: ");
                saldos[indexParaPesquisar] += double.Parse(Console.ReadLine());
                
                WriteColor("\nDepósito efetuado.", ConsoleColor.Green);

                Console.WriteLine("\nPressione qualquer tecla para continuar...");
                Console.ReadKey();
                Console.Clear();
            }

        }

        public static void FazerPIX(List<string> cpfs, List<string> titulares, List<double> saldos, List<string> senhas, List<string> chavesPIX) {
            Console.Write("\nDigite o seu CPF: ");
            string cpfParaApresentar = Console.ReadLine();
            int indexParaPesquisar = cpfs.FindIndex(cpf => cpf == cpfParaApresentar);

            if (indexParaPesquisar == -1) {
                WriteColor("\nConta não encontrada", ConsoleColor.Red);
                Console.WriteLine("Aperte qualquer tecla para continuar.");
                Console.ReadKey();
                Console.Clear();
            }
            else {
                Console.Write("\nDigite a chave PIX do titular:");
                string chaveTitularParaApresentar = Console.ReadLine();
                int indexTitularParaPesquisar = chavesPIX.FindIndex(cpf => cpf == chaveTitularParaApresentar);

                if (indexTitularParaPesquisar == -1) {
                    WriteColor("\nConta não encontrada.\nConfira a chave e tente novamente.", ConsoleColor.Red);
                }
                else {
                    Console.Write("\nDigite a quantia a ser transferida no formato: 0000,00: ");
                    double valor = double.Parse(Console.ReadLine());
                    
                    if (saldos[indexParaPesquisar] - valor >= 0) {
                        saldos[indexParaPesquisar] -= valor;
                        saldos[indexTitularParaPesquisar] += valor;
                        WriteColor($"\nTransferência PIX para {titulares[indexTitularParaPesquisar]} efetuado.", ConsoleColor.Green);
                    }
                    else {
                        WriteColor($"\nSaldo insuficiente.\nSeu saldo atual é {saldos[indexParaPesquisar]:F2}", ConsoleColor.Red);
                    }
                    

                }
            }
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
        }


        public static void ReceberPIX(List<string> cpfs, List<string> titulares, List<double> saldos, List<string> senhas, List<string> chavesPIX) {
            Console.Write("\nDigite o CPF do titular: ");
            string cpfParaApresentar = Console.ReadLine();
            int indexParaPesquisar = cpfs.FindIndex(cpf => cpf == cpfParaApresentar);

            if (indexParaPesquisar == -1) {
                WriteColor("\nConta não encontrada.\nConfira o CPF e tente novamente.", ConsoleColor.Red);
            }
            else {
                Console.Write("\nDigite sua senha:");
                string senhaParaApresentar = GetPass();
                int indexSenhaParaPesquisar = senhas.FindIndex(senha => senha == senhaParaApresentar);

                int contSenhaErrada = 3;
                if (indexSenhaParaPesquisar == -1) {
                    contSenhaErrada -= 1;
                    WriteColor($"\nSenha incorreta. Tente novamente.\nRestam {contSenhaErrada} tentativas.", ConsoleColor.Red);
                }
                else {
                    if (chavesPIX[indexParaPesquisar] == "0") {
                        chavesPIX[indexParaPesquisar] = RandomString(16);
                    }
                    
                    Console.WriteLine("\nEsta é a sua chave PIX gerada aleatoriamente.\nCompartilhe ela para receber um PIX:");
                    WriteColor($"\n{chavesPIX[indexParaPesquisar]}", ConsoleColor.Green);
                }
            }
            Console.WriteLine("\nAperte qualquer tecla para continuar.");
            Console.ReadKey();
            Console.Clear();

        }

        private static Random random = new Random();

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static void Main(string[] args) {

            List<string> cpfs = new List<string>();
            List<string> titulares = new List<string>();
            List<string> senhas = new List<string>();
            List<double> saldos = new List<double>();
            List<string> chavesPIX = new List<string>();

            int option = 0;

            do {
                showMenu();
                
                try {
                    option = int.Parse(Console.ReadLine());
                }
                catch (Exception e) {
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
                        ApresentarUsuario(cpfs, titulares, saldos);
                        break;
                    case 5:
                        QuantiaArmazenada(cpfs, titulares, saldos);
                        break;
                    case 6:
                        do {
                            ManipularConta(cpfs, titulares, saldos);

                            try {
                                option = int.Parse(Console.ReadLine());
                            }
                            catch (Exception e) {
                                option = 10;
                            }
                            
                            switch (option) {
                                case 1:
                                    FazerDeposito(cpfs, titulares, saldos);
                                    break;
                                case 2:
                                    FazerSaque(cpfs, titulares, saldos, senhas);
                                    break;
                                case 3:
                                    FazerPIX(cpfs, titulares, saldos, senhas, chavesPIX);
                                    break;
                                case 4:
                                    ReceberPIX(cpfs, titulares, saldos, senhas, chavesPIX);
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