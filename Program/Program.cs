using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Program
{
    class Program
    {
        static void Main(string[] args)
        {
            //Reqlist();                                        // Chamamos o método que traz toda a Lista
            Console.Write("Qual id você desea Chamar?: ");      // Pergunta ao usuário qual ID deseja Chamar
            String id = Console.ReadLine();                     // Armazena a resposta do Usuário
            ReqUnica(id);                                       // Chamamos uma requisição unica
            Console.ReadLine();
        }

        static void Reqlist()
        {
            // Criando uma requisicao para o link abaixo
            var requisicao = WebRequest.Create("https://jsonplaceholder.typicode.com/todos");

            // Salvando na variavel o método que iremos utilizar
            requisicao.Method = "GET";
            var RESPOSTA = requisicao.GetResponse();            // Armazenando a resposta da requisição

            // Armazena em RESPOSTA, a resposta da requisição
            using (RESPOSTA)
            {
                var stream = RESPOSTA.GetResponseStream();      // Pegando a resposta do site, e decodificando ela
                StreamReader leitor = new StreamReader(stream); // Pega os dados da resposta, e coloca dentro de um leitor
                object dados = leitor.ReadToEnd();              // Estamos de fato pegando os dados

                // Irá pegar toda a lista e irá converter para lista C#
                List<Tarefa> tarefas = JsonConvert.DeserializeObject<List<Tarefa>>(dados.ToString());


                // Para quando
                // Objeto Tarefa
                // Variavel tarefa dentro da lista de Tarefas;
                foreach (Tarefa tarefa in tarefas)
                {
                    tarefa.Exibir();                            // Exiba as tarefas
                }

                stream.Close();                                 // Toda Stream que crio, tem que ser fechada
                RESPOSTA.Close();                               // Dizendo para o servidor que não queremos mais dados
            }
        }

        static void ReqUnica(String id)
        {
            // Criando uma requisicao para o link abaixo
            var requisicao = WebRequest.Create("https://jsonplaceholder.typicode.com/todos/"+id);

            // Salvando na variavel o método que iremos utilizar
            requisicao.Method = "GET";
            var resposta = requisicao.GetResponse();            // Armazenando a resposta da requisição
            
            using (resposta)
            {
                var stream = resposta.GetResponseStream();      // Pegando a resposta do site, e decodificando ela
                StreamReader leitor = new StreamReader(stream); // Pega os dados da resposta, e coloca dentro de um leitor
                object dados = leitor.ReadToEnd();              // Estamos de fato pegando os dados

                // Irá pegar os dados, e armazenar no objeto Tarefa
                Tarefa tarefa = JsonConvert.DeserializeObject<Tarefa>(dados.ToString());

                tarefa.Exibir();                                // Irá chamar o objeto Exibir da tarefa          

                stream.Close();                                 // Toda Stream que crio, tem que ser fechada
                resposta.Close();                               // Dizendo para o servidor que não queremos mais dados
            }
        }

    }
}
