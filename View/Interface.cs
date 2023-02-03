using ByteBank1.Model;

namespace ByteBank1.View;

public class Interface {


    public static bool Login(List<string> cpfs, List<string> senhas, bool logged, int indexParaPesquisar)
        {
            do {
                if (logged) return true;
                Console.Clear();
                WriteColor("=== Login ===\n", ConsoleColor.Blue);
                WriteColor("Digite seu CPF: ", ConsoleColor.Green);
                
                string cpf = "";

                try {
                    cpf = Console.ReadLine()!;
                }
                catch (Exception) {
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
                            string senhaParaApresentar = Conta.GetPass();
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

        public static void ApresentarUsuario(List<string> cpfs, List<string> titulares, List<double> saldos) {
            
            Console.Write("Digite o cpf: ");
            string cpfParaApresentar = Console.ReadLine()!;
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

        public static void Soma(List<double> saldos) {
            Console.WriteLine($"Total acumulado no banco: R$ {saldos.Sum()}");
        }

        public static void WriteColor(string txt, ConsoleColor color) {
            
            Console.ForegroundColor = color;
            Console.WriteLine(txt);
            Console.ForegroundColor = ConsoleColor.DarkGreen;
        }
}