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
            Console.Clear();
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
                    Console.WriteLine($"CPF = {cpfs[i]} \t|\t Titular = {titulares[i]}\t |\t Saldo = R$ {saldos[i]}");
                }
            }
            else {
                WriteColor("Não há contas registradas atualmente.", ConsoleColor.Red);
            }
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }

        public static void Soma(List<double> saldos) {
            Console.WriteLine($"Total acumulado no banco: R$ {saldos.Sum()}");
        }

        public static void ApresentarUsuario(List<string> cpfs, List<string> titulares, List<double> saldos) {
            
            Console.Write("Digite o cpf: ");
            string cpfParaApresentar = Console.ReadLine();
            int indexParaPesquisar = cpfs.FindIndex(cpf => cpf == cpfParaApresentar);

            if (indexParaPesquisar == -1) {
                WriteColor("\nNão foi possivel apresentar esta conta.\nMOTIVO: Conta não encontrada.", ConsoleColor.Red);
            }
            else {
                Console.WriteLine($"\nCPF = {cpfs[indexParaPesquisar]} \t|\t Titular = {titulares[indexParaPesquisar]}\t |\t Saldo = R$ {saldos[indexParaPesquisar]}");
                Console.WriteLine("\nPressione qualquer tecla para continuar...");
                Console.ReadKey();
            }
        }

        public static void apresentaConta( int index, List<string> cpfs, List<string> titulares, List<double> saldos) {
            Console.WriteLine($"CPF = {cpfs[index]} | Titular = {titulares[index]} | Saldo = R$ {saldos[index]}");
        }

        public static void QuantiaArmazenada(List<string> cpfs, List<string> titulares, List<double> saldos)
        {
            Console.WriteLine();
            Soma(saldos);
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
        }

        public static void ManipularConta(List<string> cpfs, List<string> titulares, List<double> saldos, List<string> senhas, List<string> chavesPIX, bool logged, int indexParaPesquisar) {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\n----------------------------------------");
            Console.WriteLine("[1] - Fazer um depósito");
            Console.WriteLine("[2] - Fazer um saque");
            Console.WriteLine("[3] - Fazer um PIX");
            Console.WriteLine("[4] - Gerar chave PIX");
            Console.WriteLine("[5] - Fazer Logout");
            Console.WriteLine("[0] - Voltar para o menu anterior");
            Console.WriteLine("----------------------------------------");
            Console.Write("\nDigite a opção desejada: ");
        }


        private static bool Login(List<string> cpfs, List<string> senhas, bool logged, int indexParaPesquisar)
        {
            do {
                if (logged) return true;
                Console.Clear();
                WriteColor("=== Login ===\n", ConsoleColor.Blue);
                WriteColor("Digite seu CPF: ", ConsoleColor.Green);
                
                string cpf = "";

                try {
                    cpf = Console.ReadLine();
                }
                catch (Exception e) {
                    WriteColor("\nOcorreu um erro. Tente novamente.", ConsoleColor.Red);
                }
                
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
                    string cpfParaApresentar = cpf;
                    indexParaPesquisar = cpfs.FindIndex(cpf => cpf == cpfParaApresentar);

                    if (indexParaPesquisar == -1) {
                        WriteColor("\nConta não encontrada.\nConfira o CPF e tente novamente.", ConsoleColor.Red);
                        Console.WriteLine("Aperte qualquer tecla para continuar.");
                        Console.ReadKey();
                        logged = false;
                        return false;
                    }
                    else {
                        int contSenhaErrada = 3;
                        do {
                            Console.Write("Digite sua senha: ");
                            string senhaParaApresentar = GetPass();
                            int indexSenhaParaPesquisar = senhas.FindIndex(senha => senha == senhaParaApresentar);
                        
                            if (indexSenhaParaPesquisar == -1) {
                                contSenhaErrada -= 1;
                                WriteColor($"\nSenha incorreta. Tente novamente.\nRestam {contSenhaErrada} tentativas.", ConsoleColor.Red);
                                Console.WriteLine("Aperte qualquer tecla para continuar.");
                                Console.ReadKey();
                                Console.Clear();
                            }
                            else {
                                logged = true;
                                return true;
                            }
                        }
                        while (contSenhaErrada != 0);
                        WriteColor("Conta bloqueada temporáriamente\nProcure uma agência para desbloquear", ConsoleColor.Red);
                        Console.WriteLine("\nAperte qualquer tecla para continuar.");
                        Console.ReadKey();
                        return false;
                    }
                }
            }
            while (true);

        }


        public static void WriteColor(string txt, ConsoleColor color) {
            
            Console.ForegroundColor = color;
            Console.WriteLine(txt);
            Console.ForegroundColor = ConsoleColor.DarkGreen;
        }
        
        public static void FazerSaque(List<string> cpfs, List<string> titulares, List<double> saldos, List<string> senhas, bool logged, int indexParaPesquisar)
        {
            WriteColor($"\nSacando de {titulares[indexParaPesquisar]}.", ConsoleColor.Yellow);
            Console.Write("\nDigite a quantia a ser sacada no formato: 0000,00: ");
            double valorSaque = double.Parse(Console.ReadLine());
            double valorSaldoAtualizado = saldos[indexParaPesquisar] - valorSaque;

            if (valorSaldoAtualizado >= 0) {
                saldos[indexParaPesquisar] = valorSaldoAtualizado;
                WriteColor("\nSaque efetuado.", ConsoleColor.Green);
            }
            else {
                WriteColor($"\nSaldo insuficiente.\nSeu saldo atual é R$ {saldos[indexParaPesquisar]:F2}", ConsoleColor.Red);
            }
            
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
        }

        public static void FazerDeposito(List<string> cpfs, List<string> titulares, List<double> saldos, bool logged, int indexParaPesquisar)
        {

                Console.Write($"Deseja fazer um depósito em sua conta? ({titulares[indexParaPesquisar]}) [S/N] ");
                
                string resposta = "";
                try {
                    resposta = Console.ReadLine();
                }
                catch (Exception e) {
                    WriteColor("\nOcorreu um erro. Tente novamente.", ConsoleColor.Red);
                }

                if (resposta.ToUpper() != "S") {

                    string cpf = "";
                    Console.Write("Digite o CPF do titular: ");

                    try {
                        cpf = Console.ReadLine();
                    }
                    catch (Exception e) {
                        WriteColor("\nOcorreu um erro. Tente novamente.", ConsoleColor.Red);
                    }
                    
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
                        string cpfParaApresentar = cpf;
                        indexParaPesquisar = cpfs.FindIndex(cpf => cpf == cpfParaApresentar);

                        if (indexParaPesquisar == -1) {
                            WriteColor("\nConta não encontrada.\nConfira o CPF e tente novamente.", ConsoleColor.Red);
                            Console.WriteLine("Aperte qualquer tecla para continuar.");
                            Console.ReadKey();
                        }
                        else {
                            WriteColor($"\nDepositando para {titulares[indexParaPesquisar]}.", ConsoleColor.Yellow);
                        }
                        
                    }
                }

                Console.Write("\nDigite a quantia a ser depositada no formato: 0000,00: ");

                try {
                    saldos[indexParaPesquisar] += double.Parse(Console.ReadLine());
                    WriteColor("\nDepósito efetuado.", ConsoleColor.Green);
                }
                catch (Exception e) {
                    WriteColor("\nOcorreu um erro. Por favor tente novamente.", ConsoleColor.Red);
                }

            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
        }

        public static void FazerPIX(List<string> cpfs, List<string> titulares, List<double> saldos, List<string> senhas, List<string> chavesPIX, bool logged, int indexParaPesquisar) {
            
            WriteColor($"\nFazendo PIX de {titulares[indexParaPesquisar]}.", ConsoleColor.Yellow);
            Console.Write("\nDigite a chave PIX do titular:");
            string chaveTitularParaApresentar = Console.ReadLine();
            int indexTitularParaPesquisar = chavesPIX.FindIndex(cpf => cpf == chaveTitularParaApresentar);

            if (indexTitularParaPesquisar == -1) {
                WriteColor("\nConta não encontrada.\nConfira a chave e tente novamente.", ConsoleColor.Red);
            }
            else if (indexTitularParaPesquisar == indexParaPesquisar) {
                WriteColor("Nâo é possível fazer um PIX para si mesmo.", ConsoleColor.Red);
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
                    WriteColor($"\nSaldo insuficiente.\nSeu saldo atual é R$ {saldos[indexParaPesquisar]:F2}", ConsoleColor.Red);
                }
            }
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
        }


        public static void GerarChavePIX(List<string> cpfs, List<string> titulares, List<double> saldos, List<string> senhas, List<string> chavesPIX, bool logged, int indexParaPesquisar) {
            Console.Write("\nDigite o CPF do titular: ");
            string cpfParaApresentar = Console.ReadLine();
            indexParaPesquisar = cpfs.FindIndex(cpf => cpf == cpfParaApresentar);

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


        private static bool Logout(bool logged) {
            logged = false;
            return logged;
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

            int indexParaPesquisar = 0;
            bool logged = false;

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
                            if (!logged) {
                                if (logged = Login(cpfs, senhas, logged, indexParaPesquisar)) {
                                    ManipularConta(cpfs, titulares, saldos, senhas, chavesPIX, logged, indexParaPesquisar);
                                }
                                else {
                                    logged = false;
                                    option = 10;
                                    continue;
                                }
                            }
                            else ManipularConta(cpfs, titulares, saldos, senhas, chavesPIX, logged, indexParaPesquisar);

                            try {
                                option = int.Parse(Console.ReadLine());
                            }
                            catch (Exception e) {
                                option = 10;
                            }
                            
                            switch (option) {
                                case 0:
                                    option = 9;
                                    break;
                                case 1:
                                    FazerDeposito(cpfs, titulares, saldos, logged, indexParaPesquisar);
                                    break;
                                case 2:
                                    FazerSaque(cpfs, titulares, saldos, senhas, logged, indexParaPesquisar);
                                    break;
                                case 3:
                                    FazerPIX(cpfs, titulares, saldos, senhas, chavesPIX, logged, indexParaPesquisar);
                                    break;
                                case 4:
                                    GerarChavePIX(cpfs, titulares, saldos, senhas, chavesPIX, logged, indexParaPesquisar);
                                    break;
                                case 5:
                                    logged = Logout(logged);
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