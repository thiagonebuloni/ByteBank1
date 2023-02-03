using ByteBank1.View;

namespace ByteBank1.Model;


    public class Conta
    {

        public long Id { get; set; }
        public string Cpf { get; private set; } = null!;
        public string Senha { get; private set; } = null!;
        public double Saldo { get; private set; }
        public bool EstaBloqueada { get; protected set; }

        public Conta(long id, string cpf, string senha)
        {
            Id = id;
            Cpf = cpf;
            Senha = senha;
            Saldo = 0.0;
            EstaBloqueada = false;
        }

        protected bool Depositar(double quantia)
        {
            if (EstaBloqueada) return false;

            Saldo += quantia;
            return true;
        }

        protected bool Sacar(double quantia)
        {
            // validação 1
            if (EstaBloqueada) return false;

            // validação 2
            if (Saldo < quantia) return false;

            return true;
        }

        protected bool Transferir(double quantia, Conta contaDestino)
        {
            if (contaDestino.EstaBloqueada) return false;

            if (!Sacar(quantia)) return false;

            return contaDestino.Depositar(quantia);
        }

        public static bool ValidaCpf()
        {
            string cpf = Console.ReadLine()!.Trim().Replace(".","").Replace("-","");

            if (cpf.Length != 11) return false;

            var isNumeric = int.TryParse("123", out _);

            if (!isNumeric) return false;

            return true;
        }

        public static string GetPass()
        {
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

        public static bool Login(List<string> cpfs, List<string> senhas, bool logged, int indexParaPesquisar)
        {
            do {
                if (logged) return true;
                Console.Clear();
                Interface.WriteColor("=== Login ===\n", ConsoleColor.Blue);
                Interface.WriteColor("Digite seu CPF: ", ConsoleColor.Green);
                
                string cpf = "";

                try {
                    cpf = Console.ReadLine()!;
                }
                catch (Exception) {
                    Interface.WriteColor("\nOcorreu um erro. Tente novamente.", ConsoleColor.Red);
                }
                
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
                    string cpfParaApresentar = cpf;
                    indexParaPesquisar = cpfs.FindIndex(cpf => cpf == cpfParaApresentar);

                    if (indexParaPesquisar == -1) {
                        Interface.WriteColor("\nConta não encontrada.\nConfira o CPF e tente novamente.", ConsoleColor.Red);
                        Console.WriteLine("Aperte qualquer tecla para continuar.");
                        Console.ReadKey();
                        logged = false;
                        return false;
                    }
                    else {
                        int contSenhaErrada = 3;
                        do {
                            Console.Write("Digite sua senha: ");
                            string senhaParaApresentar = Conta.GetPass();
                            int indexSenhaParaPesquisar = senhas.FindIndex(senha => senha == senhaParaApresentar);
                        
                            if (indexSenhaParaPesquisar == -1) {
                                contSenhaErrada -= 1;
                                Interface.WriteColor($"\nSenha incorreta. Tente novamente.\nRestam {contSenhaErrada} tentativas.", ConsoleColor.Red);
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
                        Interface.WriteColor("Conta bloqueada temporáriamente\nProcure uma agência para desbloquear", ConsoleColor.Red);
                        Console.WriteLine("\nAperte qualquer tecla para continuar.");
                        Console.ReadKey();
                        return false;
                    }
                }
            }
            while (true);
        }

        private static Random random = new Random();

        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static void FazerSaque(List<string> cpfs, List<string> titulares, List<double> saldos, List<string> senhas, bool logged, int indexParaPesquisar)
        {
            Interface.WriteColor($"\nSacando de {titulares[indexParaPesquisar]}.", ConsoleColor.Yellow);
            Console.Write("\nDigite a quantia a ser sacada no formato: 0000,00: ");
            double valorSaque = double.Parse(Console.ReadLine()!);
            double valorSaldoAtualizado = saldos[indexParaPesquisar] - valorSaque;

            if (valorSaldoAtualizado >= 0) {
                saldos[indexParaPesquisar] = valorSaldoAtualizado;
                Interface.WriteColor("\nSaque efetuado.", ConsoleColor.Green);
            }
            else {
                Interface.WriteColor($"\nSaldo insuficiente.\nSeu saldo atual é R$ {saldos[indexParaPesquisar]:F2}", ConsoleColor.Red);
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
                    resposta = Console.ReadLine()!;
                }
                catch (Exception) {
                    Interface.WriteColor("\nOcorreu um erro. Tente novamente.", ConsoleColor.Red);
                }

                if (resposta.ToUpper() != "S") {

                    string cpf = "";
                    Console.Write("Digite o CPF do titular: ");
                    if (!Conta.ValidaCpf())
                    {
                        Interface.WriteColor("CPF inválido!", ConsoleColor.Red);
                        Console.WriteLine("\nPressione qualquer tecla para continuar");
                        Console.ReadKey();
                    }
                    else {
                        string cpfParaApresentar = cpf;
                        indexParaPesquisar = cpfs.FindIndex(cpf => cpf == cpfParaApresentar);

                        if (indexParaPesquisar == -1) {
                            Interface.WriteColor("\nConta não encontrada.\nConfira o CPF e tente novamente.", ConsoleColor.Red);
                            Console.WriteLine("Aperte qualquer tecla para continuar.");
                            Console.ReadKey();
                        }
                        else {
                            Interface.WriteColor($"\nDepositando para {titulares[indexParaPesquisar]}.", ConsoleColor.Yellow);
                        }
                        
                    }
                }

                Console.Write("\nDigite a quantia a ser depositada no formato: 0000,00: ");

                try {
                    saldos[indexParaPesquisar] += double.Parse(Console.ReadLine()!);
                    Interface.WriteColor("\nDepósito efetuado.", ConsoleColor.Green);
                }
                catch (Exception) {
                    Interface.WriteColor("\nOcorreu um erro. Por favor tente novamente.", ConsoleColor.Red);
                }

            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
        }

        public static void FazerPIX(List<string> cpfs, List<string> titulares, List<double> saldos, List<string> senhas, List<string> chavesPIX, bool logged, int indexParaPesquisar) {
            
            Interface.WriteColor($"\nFazendo PIX de {titulares[indexParaPesquisar]}.", ConsoleColor.Yellow);
            Console.Write("\nDigite a chave PIX do titular:");
            string chaveTitularParaApresentar = Console.ReadLine()!;
            int indexTitularParaPesquisar = chavesPIX.FindIndex(cpf => cpf == chaveTitularParaApresentar);

            if (indexTitularParaPesquisar == -1) {
                Interface.WriteColor("\nConta não encontrada.\nConfira a chave e tente novamente.", ConsoleColor.Red);
            }
            else if (indexTitularParaPesquisar == indexParaPesquisar) {
                Interface.WriteColor("Nâo é possível fazer um PIX para si mesmo.", ConsoleColor.Red);
            }
            else {
                Console.Write("\nDigite a quantia a ser transferida no formato: 0000,00: ");
                double valor = double.Parse(Console.ReadLine()!);
                
                if (saldos[indexParaPesquisar] - valor >= 0) {
                    saldos[indexParaPesquisar] -= valor;
                    saldos[indexTitularParaPesquisar] += valor;
                    Interface.WriteColor($"\nTransferência PIX para {titulares[indexTitularParaPesquisar]} efetuado.", ConsoleColor.Green);
                }
                else {
                    Interface.WriteColor($"\nSaldo insuficiente.\nSeu saldo atual é R$ {saldos[indexParaPesquisar]:F2}", ConsoleColor.Red);
                }
            }
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
        }


        public static void GerarChavePIX(List<string> cpfs, List<string> titulares, List<double> saldos, List<string> senhas, List<string> chavesPIX, bool logged, int indexParaPesquisar) {
            Console.Write("\nDigite o CPF do titular: ");
            string cpfParaApresentar = Console.ReadLine()!;
            indexParaPesquisar = cpfs.FindIndex(cpf => cpf == cpfParaApresentar);

            if (indexParaPesquisar == -1) {
                Interface.WriteColor("\nConta não encontrada.\nConfira o CPF e tente novamente.", ConsoleColor.Red);
            }
            else {
                Console.Write("\nDigite sua senha:");
                string senhaParaApresentar = Conta.GetPass();
                int indexSenhaParaPesquisar = senhas.FindIndex(senha => senha == senhaParaApresentar);

                int contSenhaErrada = 3;
                if (indexSenhaParaPesquisar == -1) {
                    contSenhaErrada -= 1;
                    Interface.WriteColor($"\nSenha incorreta. Tente novamente.\nRestam {contSenhaErrada} tentativas.", ConsoleColor.Red);
                }
                else {
                    if (chavesPIX[indexParaPesquisar] == "0") {
                        chavesPIX[indexParaPesquisar] = Conta.RandomString(16);
                    }
                    
                    Console.WriteLine("\nEsta é a sua chave PIX gerada aleatoriamente.\nCompartilhe ela para receber um PIX:");
                    Interface.WriteColor($"\n{chavesPIX[indexParaPesquisar]}", ConsoleColor.Green);
                }
            }
            Console.WriteLine("\nAperte qualquer tecla para continuar.");
            Console.ReadKey();
            Console.Clear();

        }


        public static bool Logout(bool logged) {
            logged = false;
            return logged;
        }

    }