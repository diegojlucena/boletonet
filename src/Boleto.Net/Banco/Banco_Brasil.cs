using BoletoNet.EDI.Banco;
using BoletoNet.Excecoes;
using BoletoNet.Util;
using System;
using System.Globalization;
using System.Web.UI;

[assembly: WebResource("BoletoNet.Imagens.001.jpg", "image/jpg")]
namespace BoletoNet
{
    /// <summary>
    /// Classe referente ao Banco do Brasil
    /// </summary>
    internal class Banco_Brasil : AbstractBanco, IBanco
    {

        #region Variáveis

        private string _dacNossoNumero = string.Empty;
        private int _dacBoleto = 0;

        #endregion

        #region Construtores

        internal Banco_Brasil()
        {
            try
            {
                this.Codigo = 1;
                this.Digito = "9";
                this.Nome = "Banco do Brasil";
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao instanciar objeto.", ex);
            }
        }
        #endregion

        #region Métodos de Instância

        /// <summary>
        /// Validações particulares do Banco do Brasil
        /// </summary>
        public override void ValidaBoleto(Boleto boleto)
        {
            if (string.IsNullOrEmpty(boleto.Carteira))
                throw new NotImplementedException("Carteira não informada. Utilize a carteira 11, 16, 17, 17-019, 17-027, 17-051, 18, 18-019, 18-027, 18-035, 18-140, 17-159, 17-140, 17-067 ou 31.");

            //Verifica as carteiras implementadas
            if (!boleto.Carteira.Equals("11") &
                !boleto.Carteira.Equals("16") &
                !boleto.Carteira.Equals("17") &
                !boleto.Carteira.Equals("17-019") &
                !boleto.Carteira.Equals("17-027") &
                !boleto.Carteira.Equals("17-051") &
                !boleto.Carteira.Equals("17-035") &
                !boleto.Carteira.Equals("17-067") &
                !boleto.Carteira.Equals("17-140") &
                !boleto.Carteira.Equals("17-159") &
                !boleto.Carteira.Equals("17-167") &
                !boleto.Carteira.Equals("18") &
                !boleto.Carteira.Equals("18-019") &
                !boleto.Carteira.Equals("18-027") &
                !boleto.Carteira.Equals("18-035") &
                !boleto.Carteira.Equals("18-043") &
                !boleto.Carteira.Equals("18-140") &
                !boleto.Carteira.Equals("31"))

                throw new NotImplementedException("Carteira não informada. Utilize a carteira 11, 16, 17, 17-019, 17-027, 17-051, 18, 18-019, 18-027, 18-035, 18-140, 18-043, 17-159, 17-140, 17-067 ou 31.");

            //Verifica se o nosso número é válido
            if (Utils.ToString(boleto.NossoNumero) == string.Empty)
                throw new NotImplementedException("Nosso número inválido");


            #region Carteira 11
            //Carteira 18 com nosso número de 11 posições
            if (boleto.Carteira.Equals("11"))
            {
                if (!boleto.TipoModalidade.Equals("21"))
                {
                    if (boleto.NossoNumero.Length > 11)
                        throw new NotImplementedException(string.Format("Para a carteira {0}, a quantidade máxima são de 11 de posições para o nosso número", boleto.Carteira));

                    if (boleto.Cedente.Convenio.ToString().Length == 6)
                        boleto.NossoNumero = string.Format("{0}{1}", boleto.Cedente.Convenio, Utils.FormatCode(boleto.NossoNumero, 11));
                    else
                        boleto.NossoNumero = Utils.FormatCode(boleto.NossoNumero, 11);
                }
                else
                {
                    if (boleto.Cedente.Convenio.ToString().Length != 6)
                        throw new NotImplementedException(string.Format("Para a carteira {0} e o tipo da modalidade 21, o número do convênio são de 6 posições", boleto.Carteira));

                    boleto.NossoNumero = Utils.FormatCode(boleto.NossoNumero, 17);
                }
            }
            #endregion Carteira 11

            #region Carteira 16
            //Carteira 18 com nosso número de 11 posições
            if (boleto.Carteira.Equals("16"))
            {
                if (!boleto.TipoModalidade.Equals("21"))
                {
                    if (boleto.NossoNumero.Length > 11)
                        throw new NotImplementedException(string.Format("Para a carteira {0}, a quantidade máxima são de 11 de posições para o nosso número", boleto.Carteira));

                    if (boleto.Cedente.Convenio.ToString().Length == 6)
                        boleto.NossoNumero = string.Format("{0}{1}", boleto.Cedente.Convenio, Utils.FormatCode(boleto.NossoNumero, 11));
                    else
                        boleto.NossoNumero = Utils.FormatCode(boleto.NossoNumero, 11);
                }
                else
                {
                    if (boleto.Cedente.Convenio.ToString().Length != 6)
                        throw new NotImplementedException(string.Format("Para a carteira {0} e o tipo da modalidade 21, o número do convênio são de 6 posições", boleto.Carteira));

                    boleto.NossoNumero = Utils.FormatCode(boleto.NossoNumero, 17);
                }
            }
            #endregion Carteira 16

            #region Carteira 17
            //Carteira 17
            if (boleto.Carteira.Equals("17"))
            {
                switch (boleto.Cedente.Convenio.ToString().Length)
                {
                    //O BB manda como padrão 7 posições, mas é possível solicitar um convênio com 6 posições na carteira 17
                    case 6:
                        if (boleto.NossoNumero.Length > 12)
                            throw new NotImplementedException(string.Format("Para a carteira {0}, a quantidade máxima são de 12 de posições para o nosso número", boleto.Carteira));
                        boleto.NossoNumero = Utils.FormatCode(boleto.NossoNumero, 12);
                        break;
                    case 7:
                        if (boleto.NossoNumero.Length > 17)
                            throw new NotImplementedException(string.Format("Para a carteira {0}, a quantidade máxima são de 10 de posições para o nosso número", boleto.Carteira));
                        boleto.NossoNumero = string.Format("{0}{1}", boleto.Cedente.Convenio, Utils.FormatCode(boleto.NossoNumero, 10));
                        break;
                    default:
                        throw new NotImplementedException(string.Format("Para a carteira {0}, o número do convênio deve ter 6 ou 7 posições", boleto.Carteira));
                }
            }
            #endregion Carteira 17

            #region Carteira 17-019, 17-067 e 17-167
            //Carteira 17, com variação 019
            if (boleto.Carteira.Equals("17-019") || boleto.Carteira.Equals("17-067") || boleto.Carteira.Equals("17-167"))
            {
                /*
                 * Convênio de 7 posições
                 * Nosso Número com 17 posições
                 */
                if (boleto.Cedente.Convenio.ToString().Length == 7)
                {
                    if (boleto.NossoNumero.Length > 10 && (boleto.NossoNumero.Substring(0, 7) == boleto.Cedente.Convenio.ToString()))
                    {
                        boleto.NossoNumero = boleto.NossoNumero.Substring(7);
                    }
                    else if (boleto.NossoNumero.Length > 10)
                        throw new NotImplementedException(string.Format("Para a carteira {0}, a quantidade máxima são de 10 de posições para o nosso número", boleto.Carteira));

                    boleto.NossoNumero = string.Format("{0}{1}", boleto.Cedente.Convenio, Utils.FormatCode(boleto.NossoNumero, 10));
                }
                /*
                 * Convênio de 6 posições
                 * Nosso Número com 11 posições
                 */
                else if (boleto.Cedente.Convenio.ToString().Length == 6)
                {
                    //Nosso Número com 17 posições
                    if ((boleto.Cedente.Codigo.ToString().Length + boleto.NossoNumero.Length) > 11)
                        throw new NotImplementedException(string.Format("Para a carteira {0}, a quantidade máxima são de 11 de posições para o nosso número. Onde o nosso número é formado por CCCCCCNNNNN-X: C -> número do convênio fornecido pelo Banco, N -> seqüencial atribuído pelo cliente e X -> dígito verificador do “Nosso-Número”.", boleto.Carteira));

                    boleto.NossoNumero = string.Format("{0}{1}", boleto.Cedente.Convenio, Utils.FormatCode(boleto.NossoNumero, 5));
                }
                /*
                  * Convênio de 4 posições
                  * Nosso Número com 11 posições
                  */
                else if (boleto.Cedente.Convenio.ToString().Length == 4)
                {
                    if (boleto.NossoNumero.Length > 7)
                        throw new NotImplementedException(string.Format("Para a carteira {0}, a quantidade máxima são de 7 de posições para o nosso número [{1}]", boleto.Carteira, boleto.NossoNumero));

                    boleto.NossoNumero = string.Format("{0}{1}", boleto.Cedente.Convenio, Utils.FormatCode(boleto.NossoNumero, 7));
                }
                else
                    boleto.NossoNumero = Utils.FormatCode(boleto.NossoNumero, 11);
            }
            #endregion Carteira 17-019, 17-067 e 17-167

            #region Carteira 17-027, 17-051
            //Carteira 17, com variação 027, 129 e 140
            if (boleto.Carteira.Equals("17-027") || boleto.Carteira.Equals("17-051") || boleto.Carteira.Equals("17-159") || boleto.Carteira.Equals("17-140"))
            {
                /*
                 * Convênio de 7 posições
                 * Nosso Número com 17 posições
                 */
                if (boleto.Cedente.Convenio.ToString().Length == 7)
                {
                    if (boleto.NossoNumero.Length > 10)
                        throw new NotImplementedException(string.Format("Para a carteira {0}, a quantidade máxima são de 10 de posições para o nosso número", boleto.Carteira));

                    boleto.NossoNumero = string.Format("{0}{1}", boleto.Cedente.Convenio, Utils.FormatCode(boleto.NossoNumero, 10));
                }
                /*
                 * Convênio de 6 posições
                 * Nosso Número com 11 posições
                 */
                else if (boleto.Cedente.Convenio.ToString().Length == 6)
                {
                    //Nosso Número com 17 posições
                    if ((boleto.Cedente.Codigo.ToString().Length + boleto.NossoNumero.Length) > 11)
                        throw new NotImplementedException(string.Format("Para a carteira {0}, a quantidade máxima são de 11 de posições para o nosso número. Onde o nosso número é formado por CCCCCCNNNNN-X: C -> número do convênio fornecido pelo Banco, N -> seqüencial atribuído pelo cliente e X -> dígito verificador do “Nosso-Número”.", boleto.Carteira));

                    boleto.NossoNumero = string.Format("{0}{1}", boleto.Cedente.Convenio, Utils.FormatCode(boleto.NossoNumero, 5));
                }
                /*
                  * Convênio de 4 posições
                  * Nosso Número com 11 posições
                  */
                else if (boleto.Cedente.Convenio.ToString().Length == 4)
                {
                    if (boleto.NossoNumero.Length > 7)
                        throw new NotImplementedException(string.Format("Para a carteira {0}, a quantidade máxima são de 7 de posições para o nosso número [{1}]", boleto.Carteira, boleto.NossoNumero));

                    boleto.NossoNumero = string.Format("{0}{1}", boleto.Cedente.Convenio, Utils.FormatCode(boleto.NossoNumero, 7));
                }
                else
                    boleto.NossoNumero = Utils.FormatCode(boleto.NossoNumero, 11);
            }
            #endregion Carteira 17-027, 17-051

            #region Carteira 17-035
            //Carteira 17, com variação 035
            if (boleto.Carteira.Equals("17-035"))
            {
                /*
                 * Convênio de 7 posições
                 * Nosso Número com 17 posições
                 */
                if (boleto.Cedente.Convenio.ToString().Length == 7)
                {
                    if (boleto.NossoNumero.Length > 10)
                        throw new NotImplementedException(string.Format("Para a carteira {0}, a quantidade máxima são de 10 de posições para o nosso número", boleto.Carteira));

                    boleto.NossoNumero = string.Format("{0}{1}", boleto.Cedente.Convenio, Utils.FormatCode(boleto.NossoNumero, 10));
                }
                /*
                 * Convênio de 6 posições
                 * Nosso Número com 11 posições
                 */
                else if (boleto.Cedente.Convenio.ToString().Length == 6)
                {
                    if ((boleto.Cedente.Codigo.ToString().Length + boleto.NossoNumero.Length) > 11)
                        throw new NotImplementedException(string.Format("Para a carteira {0}, a quantidade máxima são de 11 de posições para o nosso número. Onde o nosso número é formado por CCCCCCNNNNN-X: C -> número do convênio fornecido pelo Banco, N -> seqüencial atribuído pelo cliente e X -> dígito verificador do “Nosso-Número”.", boleto.Carteira));

                    boleto.NossoNumero = string.Format("{0}{1}", boleto.Cedente.Convenio, Utils.FormatCode(boleto.NossoNumero, 5));
                }
                /*
                  * Convênio de 4 posições
                  * Nosso Número com 11 posições
                  */
                else if (boleto.Cedente.Convenio.ToString().Length == 4)
                {
                    if (boleto.NossoNumero.Length > 7)
                        throw new NotImplementedException(string.Format("Para a carteira {0}, a quantidade máxima são de 7 de posições para o nosso número [{1}]", boleto.Carteira, boleto.NossoNumero));

                    boleto.NossoNumero = string.Format("{0}{1}", boleto.Cedente.Convenio, Utils.FormatCode(boleto.NossoNumero, 7));
                }
                else
                    boleto.NossoNumero = Utils.FormatCode(boleto.NossoNumero, 11);
            }
            #endregion Carteira 17-035
            #region Carteira 18
            //Carteira 18 com nosso número de 11 posições
            if (boleto.Carteira.Equals("18"))
            {
                boleto.BancoCarteira.ValidaBoleto(boleto);
                boleto.BancoCarteira.FormataNossoNumero(boleto);
            }
            #endregion Carteira 18

            #region Carteira 18-019
            //Carteira 18, com varia��o 019
            if (boleto.Carteira.Equals("18-019") || boleto.Carteira.Equals("18-043"))
            {
                /*
                 * Convênio de 7 posições
                 * Nosso Número com 17 posições
                 */
                if (boleto.Cedente.Convenio.ToString().Length == 7)
                {
                    if (boleto.NossoNumero.Length > 10)
                        throw new NotImplementedException(string.Format("Para a carteira {0}, a quantidade máxima são de 10 de posições para o nosso número", boleto.Carteira));

                    boleto.NossoNumero = string.Format("{0}{1}", boleto.Cedente.Convenio, Utils.FormatCode(boleto.NossoNumero, 10));
                }
                /*
                 * Convênio de 6 posições
                 * Nosso Número com 11 posições
                 */
                else if (boleto.Cedente.Convenio.ToString().Length == 6)
                {
                    //Modalidades de Cobrança Sem Registro – Carteira 16 e 18
                    //Nosso Número com 17 posições
                    if (!boleto.TipoModalidade.Equals("21"))
                    {
                        if ((boleto.Cedente.Codigo.ToString().Length + boleto.NossoNumero.Length) > 11)
                            throw new NotImplementedException(string.Format("Para a carteira {0}, a quantidade máxima são de 11 de posições para o nosso número. Onde o nosso número é formado por CCCCCCNNNNN-X: C -> número do convênio fornecido pelo Banco, N -> seqüencial atribuído pelo cliente e X -> dígito verificador do “Nosso-Número”.", boleto.Carteira));

                        boleto.NossoNumero = string.Format("{0}{1}", boleto.Cedente.Convenio, Utils.FormatCode(boleto.NossoNumero, 5));
                    }
                    else
                    {
                        if (boleto.Cedente.Convenio.ToString().Length != 6)
                            throw new NotImplementedException(string.Format("Para a carteira {0} e o tipo da modalidade 21, o número do convênio são de 6 posições", boleto.Carteira));

                        boleto.NossoNumero = Utils.FormatCode(boleto.NossoNumero, 17);
                    }
                }
                /*
                  * Convênio de 4 posições
                  * Nosso Número com 11 posições
                  */
                else if (boleto.Cedente.Convenio.ToString().Length == 4)
                {
                    if (boleto.NossoNumero.Length > 7)
                        throw new NotImplementedException(string.Format("Para a carteira {0}, a quantidade máxima são de 7 de posições para o nosso número [{1}]", boleto.Carteira, boleto.NossoNumero));

                    boleto.NossoNumero = string.Format("{0}{1}", boleto.Cedente.Convenio, Utils.FormatCode(boleto.NossoNumero, 7));
                }
                else
                    boleto.NossoNumero = Utils.FormatCode(boleto.NossoNumero, 11);
            }
            #endregion Carteira 18-019


            //Para atender o cliente Fiemg foi adaptado no código na variação 18-027 as variações 18-035 e 18-140
            #region Carteira 18-027
            //Carteira 18, com variação 019
            if (boleto.Carteira.Equals("18-027"))
            {
                /*
                 * Convênio de 7 posições
                 * Nosso Número com 17 posições
                 */
                if (boleto.Cedente.Convenio.ToString().Length == 7)
                {
                    if (boleto.NossoNumero.Length > 10)
                        throw new NotImplementedException(string.Format("Para a carteira {0}, a quantidade máxima são de 10 de posições para o nosso número", boleto.Carteira));

                    boleto.NossoNumero = string.Format("{0}{1}", boleto.Cedente.Convenio, Utils.FormatCode(boleto.NossoNumero, 10));
                }
                /*
                 * Convênio de 6 posições
                 * Nosso Número com 11 posições
                 */
                else if (boleto.Cedente.Convenio.ToString().Length == 6)
                {
                    //Modalidades de Cobrança Sem Registro – Carteira 16 e 18
                    //Nosso Número com 17 posições
                    if (!boleto.TipoModalidade.Equals("21"))
                    {
                        if ((boleto.Cedente.Codigo.ToString().Length + boleto.NossoNumero.Length) > 11)
                            throw new NotImplementedException(string.Format("Para a carteira {0}, a quantidade máxima são de 11 de posições para o nosso número. Onde o nosso número é formado por CCCCCCNNNNN-X: C -> número do convênio fornecido pelo Banco, N -> seqüencial atribuído pelo cliente e X -> dígito verificador do “Nosso-Número”.", boleto.Carteira));

                        boleto.NossoNumero = string.Format("{0}{1}", boleto.Cedente.Convenio, Utils.FormatCode(boleto.NossoNumero, 5));
                    }
                    else
                    {
                        if (boleto.Cedente.Convenio.ToString().Length != 6)
                            throw new NotImplementedException(string.Format("Para a carteira {0} e o tipo da modalidade 21, o número do convênio são de 6 posições", boleto.Carteira));

                        boleto.NossoNumero = Utils.FormatCode(boleto.NossoNumero, 17);
                    }
                }
                /*
                  * Convênio de 4 posições
                  * Nosso Número com 11 posições
                  */
                else if (boleto.Cedente.Convenio.ToString().Length == 4)
                {
                    if (boleto.NossoNumero.Length > 7)
                        throw new NotImplementedException(string.Format("Para a carteira {0}, a quantidade máxima são de 7 de posições para o nosso número [{1}]", boleto.Carteira, boleto.NossoNumero));

                    boleto.NossoNumero = string.Format("{0}{1}", boleto.Cedente.Convenio, Utils.FormatCode(boleto.NossoNumero, 7));
                }
                else
                    boleto.NossoNumero = Utils.FormatCode(boleto.NossoNumero, 11);
            }
            #endregion Carteira 18-027

            #region Carteira 18-035
            //Carteira 18, com variação 019
            if (boleto.Carteira.Equals("18-035"))
            {
                /*
                 * Convênio de 7 posições
                 * Nosso Número com 17 posições
                 */
                if (boleto.Cedente.Convenio.ToString().Length == 7)
                {
                    if (boleto.NossoNumero.Length > 10)
                        throw new NotImplementedException(string.Format("Para a carteira {0}, a quantidade máxima são de 10 de posições para o nosso número", boleto.Carteira));

                    boleto.NossoNumero = string.Format("{0}{1}", boleto.Cedente.Convenio, Utils.FormatCode(boleto.NossoNumero, 10));
                }
                /*
                 * Convênio de 6 posições
                 * Nosso Número com 11 posições
                 */
                else if (boleto.Cedente.Convenio.ToString().Length == 6)
                {
                    //Modalidades de Cobrança Sem Registro – Carteira 16 e 18
                    //Nosso Número com 17 posições
                    if (!boleto.TipoModalidade.Equals("21"))
                    {
                        if ((boleto.Cedente.Codigo.ToString().Length + boleto.NossoNumero.Length) > 11)
                            throw new NotImplementedException(string.Format("Para a carteira {0}, a quantidade máxima são de 11 de posições para o nosso número. Onde o nosso número é formado por CCCCCCNNNNN-X: C -> número do convênio fornecido pelo Banco, N -> seqüencial atribuído pelo cliente e X -> dígito verificador do “Nosso-Número”.", boleto.Carteira));

                        boleto.NossoNumero = string.Format("{0}{1}", boleto.Cedente.Convenio, Utils.FormatCode(boleto.NossoNumero, 5));
                    }
                    else
                    {
                        if (boleto.Cedente.Convenio.ToString().Length != 6)
                            throw new NotImplementedException(string.Format("Para a carteira {0} e o tipo da modalidade 21, o número do convênio são de 6 posições", boleto.Carteira));

                        boleto.NossoNumero = Utils.FormatCode(boleto.NossoNumero, 17);
                    }
                }
                /*
                  * Convênio de 4 posições
                  * Nosso Número com 11 posições
                  */
                else if (boleto.Cedente.Convenio.ToString().Length == 4)
                {
                    if (boleto.NossoNumero.Length > 7)
                        throw new NotImplementedException(string.Format("Para a carteira {0}, a quantidade máxima são de 7 de posições para o nosso número [{1}]", boleto.Carteira, boleto.NossoNumero));

                    boleto.NossoNumero = string.Format("{0}{1}", boleto.Cedente.Convenio, Utils.FormatCode(boleto.NossoNumero, 7));
                }
                else
                    boleto.NossoNumero = Utils.FormatCode(boleto.NossoNumero, 11);
            }
            #endregion Carteira 18-035

            #region Carteira 18-140
            //Carteira 18, com variação 019
            if (boleto.Carteira.Equals("18-140"))
            {
                /*
                 * Convênio de 7 posições
                 * Nosso Número com 17 posições
                 */
                if (boleto.Cedente.Convenio.ToString().Length == 7)
                {
                    if (boleto.NossoNumero.Length > 10)
                        throw new NotImplementedException(string.Format("Para a carteira {0}, a quantidade máxima são de 10 de posições para o nosso número", boleto.Carteira));

                    boleto.NossoNumero = string.Format("{0}{1}", boleto.Cedente.Convenio, Utils.FormatCode(boleto.NossoNumero, 10));
                }
                /*
                 * Convênio de 6 posições
                 * Nosso Número com 11 posições
                 */
                else if (boleto.Cedente.Convenio.ToString().Length == 6)
                {
                    //Modalidades de Cobrança Sem Registro – Carteira 16 e 18
                    //Nosso Número com 17 posições
                    if (!boleto.TipoModalidade.Equals("21"))
                    {
                        if ((boleto.Cedente.Codigo.ToString().Length + boleto.NossoNumero.Length) > 11)
                            throw new NotImplementedException(string.Format("Para a carteira {0}, a quantidade máxima são de 11 de posições para o nosso número. Onde o nosso número é formado por CCCCCCNNNNN-X: C -> número do convênio fornecido pelo Banco, N -> seqüencial atribuído pelo cliente e X -> dígito verificador do “Nosso-Número”.", boleto.Carteira));

                        boleto.NossoNumero = string.Format("{0}{1}", boleto.Cedente.Convenio, Utils.FormatCode(boleto.NossoNumero, 5));
                    }
                    else
                    {
                        if (boleto.Cedente.Convenio.ToString().Length != 6)
                            throw new NotImplementedException(string.Format("Para a carteira {0} e o tipo da modalidade 21, o número do convênio são de 6 posições", boleto.Carteira));

                        boleto.NossoNumero = Utils.FormatCode(boleto.NossoNumero, 17);
                    }
                }
                /*
                  * Convênio de 4 posições
                  * Nosso Número com 11 posições
                  */
                else if (boleto.Cedente.Convenio.ToString().Length == 4)
                {
                    if (boleto.NossoNumero.Length > 7)
                        throw new NotImplementedException(string.Format("Para a carteira {0}, a quantidade máxima são de 7 de posições para o nosso número [{1}]", boleto.Carteira, boleto.NossoNumero));

                    boleto.NossoNumero = string.Format("{0}{1}", boleto.Cedente.Convenio, Utils.FormatCode(boleto.NossoNumero, 7));
                }
                else
                    boleto.NossoNumero = Utils.FormatCode(boleto.NossoNumero, 11);
            }
            #endregion Carteira 18-140

            #region Carteira 31
            //Carteira 31
            if (boleto.Carteira.Equals("31"))
            {
                switch (boleto.Cedente.Convenio.ToString().Length)
                {
                    //O BB manda como padrão 7 posições, mas é possível solicitar um convênio com 6 posições na carteira 31
                    case 5:
                        if (boleto.NossoNumero.Length > 12)
                            throw new NotImplementedException(string.Format("Para a carteira {0}, a quantidade máxima são de 12 de posições para o nosso número", boleto.Carteira));
                        boleto.NossoNumero = Utils.FormatCode(boleto.NossoNumero, 12);
                        break;
                    case 6:
                        if (boleto.NossoNumero.Length > 12)
                            throw new NotImplementedException(string.Format("Para a carteira {0}, a quantidade máxima são de 12 de posições para o nosso número", boleto.Carteira));
                        boleto.NossoNumero = Utils.FormatCode(boleto.NossoNumero, 12);
                        break;
                    case 7:
                        if (boleto.NossoNumero.Length > 17)
                            throw new NotImplementedException(string.Format("Para a carteira {0}, a quantidade máxima são de 10 de posições para o nosso número", boleto.Carteira));
                        boleto.NossoNumero = string.Format("{0}{1}", boleto.Cedente.Convenio, Utils.FormatCode(boleto.NossoNumero, 10));
                        break;
                    default:
                        throw new NotImplementedException(string.Format("Para a carteira {0}, o número do convênio deve ter 6 ou 7 posições", boleto.Carteira));
                }
            }
            #endregion Carteira 31


            #region Agência e Conta Corrente
            //Verificar se a Agencia esta correta
            if (boleto.Cedente.ContaBancaria.Agencia.Length > 4)
                throw new NotImplementedException("A quantidade de dígitos da Agência " + boleto.Cedente.ContaBancaria.Agencia + ", são de 4 números.");
            else if (boleto.Cedente.ContaBancaria.Agencia.Length < 4)
                boleto.Cedente.ContaBancaria.Agencia = Utils.FormatCode(boleto.Cedente.ContaBancaria.Agencia, 4);

            //Verificar se a Conta esta correta
            if (boleto.Cedente.ContaBancaria.Conta.Length > 8)
                throw new NotImplementedException("A quantidade de dígitos da Conta " + boleto.Cedente.ContaBancaria.Conta + ", são de 8 números.");
            else if (boleto.Cedente.ContaBancaria.Conta.Length < 8)
                boleto.Cedente.ContaBancaria.Conta = Utils.FormatCode(boleto.Cedente.ContaBancaria.Conta, 8);
            #endregion Agência e Conta Corrente

            //Atribui o nome do banco ao local de pagamento
            //Atribui o nome do banco ao local de pagamento
            if (string.IsNullOrEmpty(boleto.LocalPagamento))
                boleto.LocalPagamento = "Até o vencimento, preferencialmente no " + Nome;
            else if (boleto.LocalPagamento == "Até o vencimento, preferencialmente no ")
                boleto.LocalPagamento += Nome;

            //Verifica se data do processamento é valida
            //if (boleto.DataProcessamento.ToString("dd/MM/yyyy") == "01/01/0001")
            if (boleto.DataProcessamento == DateTime.MinValue) // diegomodolo (diego.ribeiro@nectarnet.com.br)
                boleto.DataProcessamento = DateTime.Now;

            //Verifica se data do documento é valida
            //if (boleto.DataDocumento.ToString("dd/MM/yyyy") == "01/01/0001")
            if (boleto.DataDocumento == DateTime.MinValue) // diegomodolo (diego.ribeiro@nectarnet.com.br)
                boleto.DataDocumento = DateTime.Now;

            boleto.QuantidadeMoeda = 0;

            FormataCodigoBarra(boleto);
            FormataLinhaDigitavel(boleto);
            FormataNossoNumero(boleto);
        }

        # endregion

        private static string LimparCarteira(string carteira)
        {
            return carteira.Split('-')[0];
        }

        #region Métodos de formatação do boleto

        public override void FormataCodigoBarra(Boleto boleto)
        {
            string valorBoleto = boleto.ValorBoleto.ToString("f").Replace(",", "").Replace(".", "");
            valorBoleto = Utils.FormatCode(valorBoleto, 10);

            //Criada por AFK
            #region Carteira 11
            if (boleto.Carteira.Equals("11"))
            {
                if (boleto.Cedente.Convenio.ToString().Length == 6)
                {
                    if (boleto.TipoModalidade.Equals("21"))
                        boleto.CodigoBarra.Codigo = string.Format("{0}{1}{2}{3}{4}{5}{6}",
                            Utils.FormatCode(Codigo.ToString(), 3),
                            boleto.Moeda,
                            FatorVencimento(boleto),
                            valorBoleto,
                            boleto.Cedente.Convenio,
                            boleto.NossoNumero,
                            "21");
                }
                else
                {
                    boleto.CodigoBarra.Codigo = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}",
                        Utils.FormatCode(Codigo.ToString(), 3),
                        boleto.Moeda,
                        FatorVencimento(boleto),
                        valorBoleto,
                        boleto.NossoNumero,
                        boleto.Cedente.ContaBancaria.Agencia,
                        boleto.Cedente.ContaBancaria.Conta,
                        boleto.Carteira);
                }
            }
            #endregion Carteira 11

            #region Carteira 16
            if (boleto.Carteira.Equals("16"))
            {
                if (boleto.Cedente.Convenio.ToString().Length == 6 && boleto.TipoModalidade.Equals("21"))
                {

                    boleto.CodigoBarra.Codigo = string.Format("{0}{1}{2}{3}{4}{5}{6}",
                        Utils.FormatCode(Codigo.ToString(), 3),
                        boleto.Moeda,
                        FatorVencimento(boleto),
                        valorBoleto,
                        boleto.Cedente.Convenio,
                        boleto.NossoNumero,
                        "21");
                }
                else
                {
                    boleto.CodigoBarra.Codigo = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}",
                        Utils.FormatCode(Codigo.ToString(), 3),
                        boleto.Moeda,
                        FatorVencimento(boleto),
                        valorBoleto,
                        boleto.NossoNumero,
                        boleto.Cedente.ContaBancaria.Agencia,
                        boleto.Cedente.ContaBancaria.Conta,
                        boleto.Carteira);
                }
            }
            #endregion Carteira 16

            #region Carteira 17
            if (boleto.Carteira.Equals("17"))
            {
                if (boleto.Cedente.Convenio.ToString().Length == 7)
                {
                    boleto.CodigoBarra.Codigo = string.Format("{0}{1}{2}{3}{4}{5}{6}",
                        Utils.FormatCode(Codigo.ToString(), 3),
                        boleto.Moeda,
                        FatorVencimento(boleto),
                        valorBoleto,
                        "000000",
                        boleto.NossoNumero,
                        Utils.FormatCode(LimparCarteira(boleto.Carteira), 2));
                }
                else if (boleto.Cedente.Convenio.ToString().Length == 6)
                {
                    boleto.CodigoBarra.Codigo = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}",
                        Utils.FormatCode(Codigo.ToString(), 3),
                        boleto.Moeda,
                        FatorVencimento(boleto),
                        valorBoleto,
                        Strings.Mid(boleto.NossoNumero, 1, 11),
                        boleto.Cedente.ContaBancaria.Agencia,
                        boleto.Cedente.ContaBancaria.Conta,
                        boleto.Carteira);
                }
                else
                {
                    boleto.CodigoBarra.Codigo = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}",
                        Utils.FormatCode(Codigo.ToString(), 3),
                        boleto.Moeda,
                        FatorVencimento(boleto),
                        valorBoleto,
                        boleto.NossoNumero,
                        boleto.Cedente.ContaBancaria.Agencia,
                        boleto.Cedente.ContaBancaria.Conta,
                        boleto.Carteira);
                }
            }
            #endregion Carteira 17

            #region Carteira 17-019, 17-067 e 17-167
            if (boleto.Carteira.Equals("17-019") || boleto.Carteira.Equals("17-067") || boleto.Carteira.Equals("17-167"))
            {
                if (boleto.Cedente.Convenio.ToString().Length == 7)
                {
                    #region Especificação Convênio 7 posições
                    /*
                    Posição     Tamanho     Picture     Conteúdo
                    01 a 03         03      9(3)            Código do Banco na Câmara de Compensação = ‘001’
                    04 a 04         01      9(1)            Código da Moeda = '9'
                    05 a 05         01      9(1)            DV do Código de Barras (Anexo 10)
                    06 a 09         04      9(04)           Fator de Vencimento (Anexo 8)
                    10 a 19         10      9(08)           V(2) Valor
                    20 a 25         06      9(6)            Zeros
                    26 a 42         17      9(17)           Nosso-Número, sem o DV
                    26 a 32         9       (7)             Número do Convênio fornecido pelo Banco (CCCCCCC)
                    33 a 42         9       (10)            Complemento do Nosso-Número, sem DV (NNNNNNNNNN)
                    43 a 44         02      9(2)            Tipo de Carteira/Modalidade de Cobrança
                     */
                    #endregion Especificação Convênio 7 posições

                    boleto.CodigoBarra.Codigo = string.Format("{0}{1}{2}{3}{4}{5}{6}",
                        Utils.FormatCode(Codigo.ToString(), 3),
                        boleto.Moeda,
                        FatorVencimento(boleto),
                        valorBoleto,
                        "000000",
                        boleto.NossoNumero,
                        Utils.FormatCode(LimparCarteira(boleto.Carteira), 2));
                }
                else if (boleto.Cedente.Convenio.ToString().Length == 6)
                {
                    boleto.CodigoBarra.Codigo = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}",
                            Utils.FormatCode(Codigo.ToString(), 3),
                            boleto.Moeda,
                            FatorVencimento(boleto),
                            valorBoleto,
                            boleto.NossoNumero,
                            boleto.Cedente.ContaBancaria.Agencia,
                            boleto.Cedente.ContaBancaria.Conta,
                            LimparCarteira(boleto.Carteira));
                }
                else if (boleto.Cedente.Convenio.ToString().Length == 4)
                {
                    boleto.CodigoBarra.Codigo = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}",
                        Utils.FormatCode(Codigo.ToString(), 3),
                        boleto.Moeda,
                        FatorVencimento(boleto),
                        valorBoleto,
                        boleto.NossoNumero,
                        boleto.Cedente.ContaBancaria.Agencia,
                        boleto.Cedente.ContaBancaria.Conta,
                        LimparCarteira(boleto.Carteira));
                }
                else
                {
                    throw new Exception("Código do convênio informado é inválido. O código do convenio deve ter 4, 6, ou 7 dígitos.");
                }
            }
            #endregion Carteira 17-019, 17-067 e 17-167

            #region Carteira 17-027, 17-051, 17-140 e 17-159
            if (boleto.Carteira.Equals("17-027") || boleto.Carteira.Equals("17-051") || boleto.Carteira.Equals("17-140") || boleto.Carteira.Equals("17-159"))
            {
                if (boleto.Cedente.Convenio.ToString().Length == 7)
                {
                    #region Especificação Convênio 7 posições
                    /*
                    Posição     Tamanho     Picture     Conteúdo
                    01 a 03         03      9(3)            Código do Banco na Câmara de Compensação = ‘001’
                    04 a 04         01      9(1)            Código da Moeda = '9'
                    05 a 05         01      9(1)            DV do Código de Barras (Anexo 10)
                    06 a 09         04      9(04)           Fator de Vencimento (Anexo 8)
                    10 a 19         10      9(08)           V(2) Valor
                    20 a 25         06      9(6)            Zeros
                    26 a 42         17      9(17)           Nosso-Número, sem o DV
                    26 a 32         9       (7)             Número do Convênio fornecido pelo Banco (CCCCCCC)
                    33 a 42         9       (10)            Complemento do Nosso-Número, sem DV (NNNNNNNNNN)
                    43 a 44         02      9(2)            Tipo de Carteira/Modalidade de Cobrança
                     */
                    #endregion Especificação Convênio 7 posições

                    boleto.CodigoBarra.Codigo = string.Format("{0}{1}{2}{3}{4}{5}{6}",
                        Utils.FormatCode(Codigo.ToString(), 3),
                        boleto.Moeda,
                        FatorVencimento(boleto),
                        valorBoleto,
                        "000000",
                        boleto.NossoNumero,
                        Utils.FormatCode(LimparCarteira(boleto.Carteira), 2));
                }
                else if (boleto.Cedente.Convenio.ToString().Length == 6)
                {
                    boleto.CodigoBarra.Codigo = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}",
                            Utils.FormatCode(Codigo.ToString(), 3),
                            boleto.Moeda,
                            FatorVencimento(boleto),
                            valorBoleto,
                            boleto.NossoNumero,
                            boleto.Cedente.ContaBancaria.Agencia,
                            boleto.Cedente.ContaBancaria.Conta,
                            LimparCarteira(boleto.Carteira));
                }
                else if (boleto.Cedente.Convenio.ToString().Length == 4)
                {
                    boleto.CodigoBarra.Codigo = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}",
                        Utils.FormatCode(Codigo.ToString(), 3),
                        boleto.Moeda,
                        FatorVencimento(boleto),
                        valorBoleto,
                        boleto.NossoNumero,
                        boleto.Cedente.ContaBancaria.Agencia,
                        boleto.Cedente.ContaBancaria.Conta,
                        LimparCarteira(boleto.Carteira));
                }
                else
                {
                    throw new Exception("Código do convênio informado é inválido. O código do convenio deve ter 4, 6, ou 7 dígitos.");
                }
            }
            #endregion Carteira 17-027, 17-051, 17-140 e 17-159


            #region Carteira 17-035
            if (boleto.Carteira.Equals("17-035"))
            {
                if (boleto.Cedente.Convenio.ToString().Length == 7)
                {
                    #region Especificação Convênio 7 posições
                    /*
                    Posição     Tamanho     Picture     Conteúdo
                    01 a 03         03      9(3)            Código do Banco na Câmara de Compensação = ‘001’
                    04 a 04         01      9(1)            Código da Moeda = '9'
                    05 a 05         01      9(1)            DV do Código de Barras (Anexo 10)
                    06 a 09         04      9(04)           Fator de Vencimento (Anexo 8)
                    10 a 19         10      9(08)           V(2) Valor
                    20 a 25         06      9(6)            Zeros
                    26 a 42         17      9(17)           Nosso-Número, sem o DV
                    26 a 32         9       (7)             Número do Convênio fornecido pelo Banco (CCCCCCC)
                    33 a 42         9       (10)            Complemento do Nosso-Número, sem DV (NNNNNNNNNN)
                    43 a 44         02      9(2)            Tipo de Carteira/Modalidade de Cobrança
                     */
                    #endregion Especificação Convênio 7 posições

                    boleto.CodigoBarra.Codigo = string.Format("{0}{1}{2}{3}{4}{5}{6}",
                        Utils.FormatCode(Codigo.ToString(), 3),
                        boleto.Moeda,
                        FatorVencimento(boleto).ToString("0000"),
                        valorBoleto,
                        "000000",
                        boleto.NossoNumero,
                        Utils.FormatCode(LimparCarteira(boleto.Carteira), 2));
                }
                else if (boleto.Cedente.Convenio.ToString().Length == 6)
                {
                    boleto.CodigoBarra.Codigo = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}",
                            Utils.FormatCode(Codigo.ToString(), 3),
                            boleto.Moeda,
                            FatorVencimento(boleto),
                            valorBoleto,
                            boleto.NossoNumero,
                            boleto.Cedente.ContaBancaria.Agencia,
                            boleto.Cedente.ContaBancaria.Conta,
                            LimparCarteira(boleto.Carteira));
                }
                else if (boleto.Cedente.Convenio.ToString().Length == 4)
                {
                    boleto.CodigoBarra.Codigo = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}",
                        Utils.FormatCode(Codigo.ToString(), 3),
                        boleto.Moeda,
                        FatorVencimento(boleto),
                        valorBoleto,
                        boleto.NossoNumero,
                        boleto.Cedente.ContaBancaria.Agencia,
                        boleto.Cedente.ContaBancaria.Conta,
                        LimparCarteira(boleto.Carteira));
                }
                else
                {
                    throw new Exception("Código do convênio informado é inválido. O código do convenio deve ter 4, 6, ou 7 dígitos.");
                }
            }
            #endregion Carteira 17-035
            #region Carteira 18
            if (boleto.Carteira.Equals("18"))
            {
                boleto.BancoCarteira.FormataCodigoBarra(boleto);
            }
            #endregion Carteira 18

            #region Carteira 18-019
            if (boleto.Carteira.Equals("18-019") || boleto.Carteira.Equals("18-043"))
            {
                if (boleto.Cedente.Convenio.ToString().Length == 7)
                {
                    #region Especificação Convênio 7 posições
                    /*
                    Posição     Tamanho     Picture     Conteúdo
                    01 a 03         03      9(3)            Código do Banco na Câmara de Compensação = ‘001’
                    04 a 04         01      9(1)            Código da Moeda = '9'
                    05 a 05         01      9(1)            DV do Código de Barras (Anexo 10)
                    06 a 09         04      9(04)           Fator de Vencimento (Anexo 8)
                    10 a 19         10      9(08)           V(2) Valor
                    20 a 25         06      9(6)            Zeros
                    26 a 42         17      9(17)           Nosso-Número, sem o DV
                    26 a 32         9       (7)             Número do Convênio fornecido pelo Banco (CCCCCCC)
                    33 a 42         9       (10)            Complemento do Nosso-Número, sem DV (NNNNNNNNNN)
                    43 a 44         02      9(2)            Tipo de Carteira/Modalidade de Cobrança
                     */
                    #endregion Especificação Convênio 7 posições

                    boleto.CodigoBarra.Codigo = string.Format("{0}{1}{2}{3}{4}{5}{6}",
                        Utils.FormatCode(Codigo.ToString(), 3),
                        boleto.Moeda,
                        FatorVencimento(boleto),
                        valorBoleto,
                        "000000",
                        boleto.NossoNumero,
                        Utils.FormatCode(LimparCarteira(boleto.Carteira), 2));
                }
                else if (boleto.Cedente.Convenio.ToString().Length == 6)
                {
                    if (boleto.TipoModalidade.Equals("21"))
                        boleto.CodigoBarra.Codigo = string.Format("{0}{1}{2}{3}{4}{5}{6}",
                            Utils.FormatCode(Codigo.ToString(), 3),
                            boleto.Moeda,
                            FatorVencimento(boleto),
                            valorBoleto,
                            boleto.Cedente.Convenio,
                            boleto.NossoNumero,
                            "21");
                    else
                        boleto.CodigoBarra.Codigo = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}",
                            Utils.FormatCode(Codigo.ToString(), 3),
                            boleto.Moeda,
                            FatorVencimento(boleto),
                            valorBoleto,
                            boleto.NossoNumero,
                            boleto.Cedente.ContaBancaria.Agencia,
                            boleto.Cedente.ContaBancaria.Conta,
                            LimparCarteira(boleto.Carteira));
                }
                else if (boleto.Cedente.Convenio.ToString().Length == 4)
                {
                    boleto.CodigoBarra.Codigo = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}",
                        Utils.FormatCode(Codigo.ToString(), 3),
                        boleto.Moeda,
                        FatorVencimento(boleto),
                        valorBoleto,
                        boleto.NossoNumero,
                        boleto.Cedente.ContaBancaria.Agencia,
                        boleto.Cedente.ContaBancaria.Conta,
                        LimparCarteira(boleto.Carteira));
                }
                else
                {
                    throw new Exception("Código do convênio informado é inválido. O código do convenio deve ter 4, 6, ou 7 dígitos.");
                }

            }
            #endregion Carteira 18-019

            //Para atender o cliente Fiemg foi adptado no código na variação 18-027 as variações 18-035 e 18-140
            #region Carteira 18-027
            if (boleto.Carteira.Equals("18-027"))
            {
                if (boleto.Cedente.Convenio.ToString().Length == 7)
                {
                    #region Especificação Convênio 7 posições
                    /*
                    Posição     Tamanho     Picture     Conteúdo
                    01 a 03         03      9(3)            Código do Banco na Câmara de Compensação = ‘001’
                    04 a 04         01      9(1)            Código da Moeda = '9'
                    05 a 05         01      9(1)            DV do Código de Barras (Anexo 10)
                    06 a 09         04      9(04)           Fator de Vencimento (Anexo 8)
                    10 a 19         10      9(08)           V(2) Valor
                    20 a 25         06      9(6)            Zeros
                    26 a 42         17      9(17)           Nosso-Número, sem o DV
                    26 a 32         9       (7)             Número do Convênio fornecido pelo Banco (CCCCCCC)
                    33 a 42         9       (10)            Complemento do Nosso-Número, sem DV (NNNNNNNNNN)
                    43 a 44         02      9(2)            Tipo de Carteira/Modalidade de Cobrança
                     */
                    #endregion Especificação Convênio 7 posições

                    boleto.CodigoBarra.Codigo = string.Format("{0}{1}{2}{3}{4}{5}{6}",
                        Utils.FormatCode(Codigo.ToString(), 3),
                        boleto.Moeda,
                        FatorVencimento(boleto).ToString("0000"),
                        valorBoleto,
                        "000000",
                        boleto.NossoNumero,
                        Utils.FormatCode(LimparCarteira(boleto.Carteira), 2));
                }
                else if (boleto.Cedente.Convenio.ToString().Length == 6)
                {
                    if (boleto.TipoModalidade.Equals("21"))
                        boleto.CodigoBarra.Codigo = string.Format("{0}{1}{2}{3}{4}{5}{6}",
                            Utils.FormatCode(Codigo.ToString(), 3),
                            boleto.Moeda,
                            FatorVencimento(boleto),
                            valorBoleto,
                            boleto.Cedente.Convenio,
                            boleto.NossoNumero,
                            "21");
                    else
                        boleto.CodigoBarra.Codigo = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}",
                            Utils.FormatCode(Codigo.ToString(), 3),
                            boleto.Moeda,
                            FatorVencimento(boleto),
                            valorBoleto,
                            boleto.NossoNumero,
                            boleto.Cedente.ContaBancaria.Agencia,
                            boleto.Cedente.ContaBancaria.Conta,
                            LimparCarteira(boleto.Carteira));
                }
                else if (boleto.Cedente.Convenio.ToString().Length == 4)
                {
                    boleto.CodigoBarra.Codigo = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}",
                        Utils.FormatCode(Codigo.ToString(), 3),
                        boleto.Moeda,
                        FatorVencimento(boleto),
                        valorBoleto,
                        boleto.NossoNumero,
                        boleto.Cedente.ContaBancaria.Agencia,
                        boleto.Cedente.ContaBancaria.Conta,
                        LimparCarteira(boleto.Carteira));
                }
                else
                {
                    throw new Exception("Código do convênio informado é inválido. O código do convenio deve ter 4, 6, ou 7 dígitos.");
                }
            }
            #endregion Carteira 18-027

            #region Carteira 18-035
            if (boleto.Carteira.Equals("18-035"))
            {
                if (boleto.Cedente.Convenio.ToString().Length == 7)
                {
                    #region Especificação Convênio 7 posições
                    /*
                    Posição     Tamanho     Picture     Conteúdo
                    01 a 03         03      9(3)            Código do Banco na Câmara de Compensação = ‘001’
                    04 a 04         01      9(1)            Código da Moeda = '9'
                    05 a 05         01      9(1)            DV do Código de Barras (Anexo 10)
                    06 a 09         04      9(04)           Fator de Vencimento (Anexo 8)
                    10 a 19         10      9(08)           V(2) Valor
                    20 a 25         06      9(6)            Zeros
                    26 a 42         17      9(17)           Nosso-Número, sem o DV
                    26 a 32         9       (7)             Número do Convênio fornecido pelo Banco (CCCCCCC)
                    33 a 42         9       (10)            Complemento do Nosso-Número, sem DV (NNNNNNNNNN)
                    43 a 44         02      9(2)            Tipo de Carteira/Modalidade de Cobrança
                     */
                    #endregion Especificação Convênio 7 posições

                    boleto.CodigoBarra.Codigo = string.Format("{0}{1}{2}{3}{4}{5}{6}",
                        Utils.FormatCode(Codigo.ToString(), 3),
                        boleto.Moeda,
                        FatorVencimento(boleto).ToString("0000"),
                        valorBoleto,
                        "000000",
                        boleto.NossoNumero,
                        Utils.FormatCode(LimparCarteira(boleto.Carteira), 2));
                }
                else if (boleto.Cedente.Convenio.ToString().Length == 6)
                {
                    if (boleto.TipoModalidade.Equals("21"))
                        boleto.CodigoBarra.Codigo = string.Format("{0}{1}{2}{3}{4}{5}{6}",
                            Utils.FormatCode(Codigo.ToString(), 3),
                            boleto.Moeda,
                            FatorVencimento(boleto),
                            valorBoleto,
                            boleto.Cedente.Convenio,
                            boleto.NossoNumero,
                            "21");
                    else
                        boleto.CodigoBarra.Codigo = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}",
                            Utils.FormatCode(Codigo.ToString(), 3),
                            boleto.Moeda,
                            FatorVencimento(boleto),
                            valorBoleto,
                            boleto.NossoNumero,
                            boleto.Cedente.ContaBancaria.Agencia,
                            boleto.Cedente.ContaBancaria.Conta,
                            LimparCarteira(boleto.Carteira));
                }
                else if (boleto.Cedente.Convenio.ToString().Length == 4)
                {
                    boleto.CodigoBarra.Codigo = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}",
                        Utils.FormatCode(Codigo.ToString(), 3),
                        boleto.Moeda,
                        FatorVencimento(boleto),
                        valorBoleto,
                        boleto.NossoNumero,
                        boleto.Cedente.ContaBancaria.Agencia,
                        boleto.Cedente.ContaBancaria.Conta,
                        LimparCarteira(boleto.Carteira));
                }
                else
                {
                    throw new Exception("Código do convênio informado é inválido. O código do convenio deve ter 4, 6, ou 7 dígitos.");
                }
            }
            #endregion Carteira 18-035

            #region Carteira 18-140
            if (boleto.Carteira.Equals("18-140"))
            {
                if (boleto.Cedente.Convenio.ToString().Length == 7)
                {
                    #region Especificação Convênio 7 posições
                    /*
                    Posição     Tamanho     Picture     Conteúdo
                    01 a 03         03      9(3)            Código do Banco na Câmara de Compensação = ‘001’
                    04 a 04         01      9(1)            Código da Moeda = '9'
                    05 a 05         01      9(1)            DV do Código de Barras (Anexo 10)
                    06 a 09         04      9(04)           Fator de Vencimento (Anexo 8)
                    10 a 19         10      9(08)           V(2) Valor
                    20 a 25         06      9(6)            Zeros
                    26 a 42         17      9(17)           Nosso-Número, sem o DV
                    26 a 32         9       (7)             Número do Convênio fornecido pelo Banco (CCCCCCC)
                    33 a 42         9       (10)            Complemento do Nosso-Número, sem DV (NNNNNNNNNN)
                    43 a 44         02      9(2)            Tipo de Carteira/Modalidade de Cobrança
                     */
                    #endregion Especificação Convênio 7 posições

                    boleto.CodigoBarra.Codigo = string.Format("{0}{1}{2}{3}{4}{5}{6}",
                        Utils.FormatCode(Codigo.ToString(), 3),
                        boleto.Moeda,
                        FatorVencimento(boleto).ToString("0000"),
                        valorBoleto,
                        "000000",
                        boleto.NossoNumero,
                        Utils.FormatCode(LimparCarteira(boleto.Carteira), 2));
                }
                else if (boleto.Cedente.Convenio.ToString().Length == 6)
                {
                    if (boleto.TipoModalidade.Equals("21"))
                        boleto.CodigoBarra.Codigo = string.Format("{0}{1}{2}{3}{4}{5}{6}",
                            Utils.FormatCode(Codigo.ToString(), 3),
                            boleto.Moeda,
                            FatorVencimento(boleto),
                            valorBoleto,
                            boleto.Cedente.Convenio,
                            boleto.NossoNumero,
                            "21");
                    else
                        boleto.CodigoBarra.Codigo = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}",
                            Utils.FormatCode(Codigo.ToString(), 3),
                            boleto.Moeda,
                            FatorVencimento(boleto),
                            valorBoleto,
                            boleto.NossoNumero,
                            boleto.Cedente.ContaBancaria.Agencia,
                            boleto.Cedente.ContaBancaria.Conta,
                            LimparCarteira(boleto.Carteira));
                }
                else if (boleto.Cedente.Convenio.ToString().Length == 4)
                {
                    boleto.CodigoBarra.Codigo = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}",
                        Utils.FormatCode(Codigo.ToString(), 3),
                        boleto.Moeda,
                        FatorVencimento(boleto),
                        valorBoleto,
                        boleto.NossoNumero,
                        boleto.Cedente.ContaBancaria.Agencia,
                        boleto.Cedente.ContaBancaria.Conta,
                        LimparCarteira(boleto.Carteira));
                }
                else
                {
                    throw new Exception("Código do convênio informado é inválido. O código do convenio deve ter 4, 6, ou 7 dígitos.");
                }
            }
            #endregion Carteira 18-140

            #region Carteira 31
            if (boleto.Carteira.Equals("31"))
            {
                boleto.CodigoBarra.Codigo = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}",
                    Utils.FormatCode(Codigo.ToString(), 3),
                    boleto.Moeda,
                    FatorVencimento(boleto),
                    valorBoleto,
                    boleto.NossoNumero,
                    boleto.Cedente.ContaBancaria.Agencia,
                    boleto.Cedente.ContaBancaria.Conta,
                    boleto.Carteira);
            }
            #endregion Carteira 31

            _dacBoleto = Mod11(boleto.CodigoBarra.Codigo, 9);

            boleto.CodigoBarra.Codigo = Strings.Left(boleto.CodigoBarra.Codigo, 4) + _dacBoleto + Strings.Right(boleto.CodigoBarra.Codigo, 39);
        }

        public override void FormataLinhaDigitavel(Boleto boleto)
        {
            string cmplivre = string.Empty;
            string campo1 = string.Empty;
            string campo2 = string.Empty;
            string campo3 = string.Empty;
            string campo4 = string.Empty;
            string campo5 = string.Empty;
            long icampo5 = 0;
            int digitoMod = 0;

            /*
            Campos 1 (AAABC.CCCCX):
            A = Código do Banco na Câmara de Compensação “001”
            B = Código da moeda "9" (*)
            C = Posição 20 a 24 do código de barras
            X = DV que amarra o campo 1 (Módulo 10, contido no Anexo 7)
             */

            cmplivre = Strings.Mid(boleto.CodigoBarra.Codigo, 20, 25);

            campo1 = Strings.Left(boleto.CodigoBarra.Codigo, 4) + Strings.Mid(cmplivre, 1, 5);
            digitoMod = Mod10(campo1);
            campo1 = campo1 + digitoMod.ToString();
            campo1 = Strings.Mid(campo1, 1, 5) + "." + Strings.Mid(campo1, 6, 5);
            /*
            Campo 2 (DDDDD.DDDDDY)
            D = Posição 25 a 34 do código de barras
            Y = DV que amarra o campo 2 (Módulo 10, contido no Anexo 7)
             */
            campo2 = Strings.Mid(cmplivre, 6, 10);
            digitoMod = Mod10(campo2);
            campo2 = campo2 + digitoMod.ToString();
            campo2 = Strings.Mid(campo2, 1, 5) + "." + Strings.Mid(campo2, 6, 6);


            /*
            Campo 3 (EEEEE.EEEEEZ)
            E = Posição 35 a 44 do código de barras
            Z = DV que amarra o campo 3 (Módulo 10, contido no Anexo 7)
             */
            campo3 = Strings.Mid(cmplivre, 16, 10);
            digitoMod = Mod10(campo3);
            campo3 = campo3 + digitoMod;
            campo3 = Strings.Mid(campo3, 1, 5) + "." + Strings.Mid(campo3, 6, 6);

            /*
            Campo 4 (K)
            K = DV do Código de Barras (Módulo 11, contido no Anexo 10)
             */
            campo4 = Strings.Mid(boleto.CodigoBarra.Codigo, 5, 1);

            /*
            Campo 5 (UUUUVVVVVVVVVV)
            U = Fator de Vencimento ( Anexo 10)
            V = Valor do Título (*)
             */
            icampo5 = Convert.ToInt64(Strings.Mid(boleto.CodigoBarra.Codigo, 6, 14));

            if (icampo5 == 0)
                campo5 = "000";
            else
                campo5 = icampo5.ToString();

            boleto.CodigoBarra.LinhaDigitavel = campo1 + " " + campo2 + " " + campo3 + " " + campo4 + " " + campo5;
        }

        /// <summary>
        /// Formata o nosso número para ser mostrado no boleto.
        /// </summary>
        /// <remarks>
        /// Última a atualização por Transis em 26/09/2011
        /// </remarks>
        /// <param name="boleto"></param>
        public override void FormataNossoNumero(Boleto boleto)
        {
            if (boleto.Cedente.Convenio.ToString().Length == 6) //somente monta o digito verificador no nosso numero se o convenio tiver 6 posições
            {
                switch (boleto.Carteira)
                {
                    case "18-019":
                        boleto.NossoNumero = string.Format("{0}/{1}-{2}", LimparCarteira(boleto.Carteira), boleto.NossoNumero, Mod11BancoBrasil(boleto.NossoNumero));
                        return;
                }
            }

            switch (boleto.Carteira)
            {
                case "17-019":
                case "17-027":
                case "17-051":
                case "17-035":
                case "17-067":
                case "17-140":
                case "17-159":
                case "17-167":
                case "18-019":
                    boleto.NossoNumero = string.Format("{0}/{1}", LimparCarteira(boleto.Carteira), boleto.NossoNumero);
                    return;
                case "31":
                    boleto.NossoNumero = string.Format("{0}{1}", Utils.FormatCode(boleto.Cedente.Convenio.ToString(), 7), boleto.NossoNumero);
                    return;
            }

            if (boleto.BancoCarteira != null)
                boleto.BancoCarteira.FormataNossoNumero(boleto);
            else
                boleto.NossoNumero = string.Format("{0}", boleto.NossoNumero);
        }


        public override void FormataNumeroDocumento(Boleto boleto)
        {
        }

        # endregion

        #region Métodos de geração do arquivo remessa - Genéricos
        /// <summary>
        /// HEADER DE LOTE do arquivo CNAB
        /// Gera o HEADER de Lote do arquivo remessa de acordo com o lay-out informado
        /// </summary>
        public override string GerarHeaderLoteRemessa(string numeroConvenio, Cedente cedente, int numeroArquivoRemessa, TipoArquivo tipoArquivo)
        {
            try
            {
                string header = " ";

                base.GerarHeaderLoteRemessa(numeroConvenio, cedente, numeroArquivoRemessa, tipoArquivo);

                switch (tipoArquivo)
                {
                    case TipoArquivo.CNAB240:
                        header = GerarHeaderLoteRemessaCNAB240(numeroConvenio, cedente, numeroArquivoRemessa);
                        break;
                    case TipoArquivo.CNAB400:
                        header = "";
                        break;
                    case TipoArquivo.Outro:
                        throw new Exception("Tipo de arquivo inexistente.");
                }

                return header;

            }
            catch (Exception ex)
            {
                throw new Exception("Erro durante a geração do HEADER DO LOTE do arquivo de REMESSA.", ex);
            }
        }
        /// <summary>
        /// HEADER do arquivo CNAB
        /// Gera o HEADER do arquivo remessa de acordo com o lay-out informado
        /// </summary>
        public override string GerarHeaderRemessa(string numeroConvenio, Cedente cedente, TipoArquivo tipoArquivo, int numeroArquivoRemessa)
        {
            try
            {
                string _header = " ";

                base.GerarHeaderRemessa(numeroConvenio, cedente, tipoArquivo, numeroArquivoRemessa);

                switch (tipoArquivo)
                {
                    case TipoArquivo.CNAB240:
                        _header = GerarHeaderRemessaCNAB240(cedente, numeroArquivoRemessa);
                        break;
                    case TipoArquivo.CNAB400:
                        _header = GerarHeaderRemessaCNAB400(cedente, numeroArquivoRemessa);
                        break;
                    case TipoArquivo.Outro:
                        throw new Exception("Tipo de arquivo inexistente.");
                }

                return _header;

            }
            catch (Exception ex)
            {
                throw new Exception("Erro durante a geração do HEADER do arquivo de REMESSA.", ex);
            }
        }
        /// <summary>
        /// Efetua as Validações dentro da classe Boleto, para garantir a geração da remessa
        /// </summary>
        public override bool ValidarRemessa(TipoArquivo tipoArquivo, string numeroConvenio, IBanco banco, Cedente cedente, Boletos boletos, int numeroArquivoRemessa, out string mensagem)
        {
            bool vRetorno = true;
            string vMsg = string.Empty;
            //            
            switch (tipoArquivo)
            {
                case TipoArquivo.CNAB240:
                    vRetorno = ValidarRemessaCNAB240(numeroConvenio, banco, cedente, boletos, numeroArquivoRemessa, out vMsg);
                    break;
                case TipoArquivo.CNAB400:
                    vRetorno = ValidarRemessaCNAB400(numeroConvenio, banco, cedente, boletos, numeroArquivoRemessa, out vMsg);
                    break;
                case TipoArquivo.Outro:
                    throw new Exception("Tipo de arquivo inexistente.");
            }
            //
            mensagem = vMsg;
            return vRetorno;
        }
        /// <summary>
        /// DETALHE do arquivo CNAB
        /// Gera o DETALHE do arquivo remessa de acordo com o lay-out informado
        /// </summary>
        public override string GerarDetalheRemessa(Boleto boleto, int numeroRegistro, TipoArquivo tipoArquivo)
        {
            try
            {
                string _detalhe = " ";

                base.GerarDetalheRemessa(boleto, numeroRegistro, tipoArquivo);

                switch (tipoArquivo)
                {
                    case TipoArquivo.CNAB240:
                        _detalhe = GerarDetalheRemessaCNAB240(boleto, numeroRegistro, tipoArquivo);
                        break;
                    case TipoArquivo.CNAB400:
                        _detalhe = GerarDetalheRemessaCNAB400(boleto, numeroRegistro, tipoArquivo);
                        break;
                    case TipoArquivo.Outro:
                        throw new Exception("Tipo de arquivo inexistente.");
                }

                return _detalhe;

            }
            catch (Exception ex)
            {
                throw new Exception("Erro durante a geração do DETALHE arquivo de REMESSA.", ex);
            }
        }
        public override string GerarDetalheSegmentoPRemessa(Boleto boleto, int numeroRegistro, string numeroConvenio)
        {
            try
            {
                string _segmentoP;
                string _nossoNumero;

                _segmentoP = "00100013";
                _segmentoP += Utils.FitStringLength(numeroRegistro.ToString(), 5, 5, '0', 0, true, true, true);
                _segmentoP += "P ";
                _segmentoP += ObterCodigoDaOcorrencia(boleto);
                _segmentoP += Utils.FitStringLength(boleto.Cedente.ContaBancaria.Agencia, 5, 5, '0', 0, true, true, true);
                _segmentoP += Utils.FitStringLength(boleto.Cedente.ContaBancaria.DigitoAgencia, 1, 1, '0', 0, true, true, true);
                _segmentoP += Utils.FitStringLength(boleto.Cedente.ContaBancaria.Conta, 12, 12, '0', 0, true, true, true);
                _segmentoP += Utils.FitStringLength(boleto.Cedente.ContaBancaria.DigitoConta, 1, 1, '0', 0, true, true, true);
                // Dígito verificador  da Agência/Conta.  Campo não tratado pelo BB.  Informar ESPAÇO ou ZERO.
                _segmentoP += " "; // jefhtavares O banco não aceita mais esse campo como 0 (zero), o campo deverá ser enviado em branco

                //=====================================================================================================
                //Ajustes efetuados de acordo com manual Julho/2011 - Retirado por jsoda - em 11/05/2012
                //
                //int totalCaracteres = numeroConvenio.Length - 9;
                //_segmentoP += numeroConvenio.Substring(0, totalCaracteres);

                //_nossoNumero = Utils.FitStringLength(boleto.NumeroDocumento, 10, 10, '0', 0, true, true, true);
                //int _total = numeroConvenio.Substring(0, totalCaracteres).Length + _nossoNumero.Length;
                //int subtotal = 0;
                //subtotal = 20 - _total;
                //string _comnplemento = new string(' ', subtotal);
                //_segmentoP += _nossoNumero;
                //_segmentoP += _comnplemento;
                //=====================================================================================================

                switch (boleto.Cedente.Convenio.ToString().Length)
                {
                    case 4:
                        // Se convênio de 4 posições - normalmente carteira 17 - (0001 à 9999), informar NossoNumero com 11 caracteres, com DV, sendo:
                        // 4 posições do nº do convênio e 7 posições do nº de controle (nº do documento) e DV.
                        _nossoNumero = string.Format("{0}{1}{2}", boleto.Cedente.Convenio, Utils.FormatCode(boleto.NossoNumero, 7), Mod11BancoBrasil(boleto.NossoNumero));
                        break;
                    case 6:
                        // Se convênio de 6 posições (acima de 10.000 à 999.999), informar NossoNumero com 11 caracteres + DV, sendo:
                        // 6 posições do nº do convênio e 5 posições do nº de controle (nº do documento) e DV do nosso numero.
                        if (boleto.NossoNumero.Length != 11)
                            _nossoNumero = string.Format("{0}{1}{2}", boleto.Cedente.Convenio, Utils.FormatCode(boleto.NossoNumero, 5), Mod11BancoBrasil(boleto.NossoNumero));
                        else
                            _nossoNumero = boleto.NossoNumero + Mod11BancoBrasil(boleto.NossoNumero);
                        break;
                    case 7:
                        // Se convênio de 7 posições (acima de 1.000.000 à 9.999.999), informar NossoNumero com 17 caracteres, sem DV, sendo:
                        // 7 posições do nº do convênio e 10 posições do nº de controle (nº do documento)
                        //_nossoNumero = string.Format("{0}{1}", boleto.Cedente.Convenio, Utils.FormatCode(boleto.NossoNumero, 7));
                        //ALTERADO POR MARCELHSOUZA EM 28/03/2013
                        if (boleto.NossoNumero.Length != 17)
                            _nossoNumero = string.Format("{0}{1}", boleto.Cedente.Convenio, Utils.FormatCode(boleto.NossoNumero, 10)); //sao 10 digitos pro no. sequencial
                        else
                            _nossoNumero = boleto.NossoNumero;
                        break;
                    default:
                        throw new Exception("Posições do nº de convênio deve ser 4, 6 ou 7.");
                }

                // Importante: Nosso número, alinhar à esquerda com brancos à direita (conforme manual)
                _segmentoP += Utils.FitStringLength(_nossoNumero, 20, 20, ' ', 0, true, true, false);

                // Informar 1 – para carteira 11/12 na modalidade Simples; 
                // 2 ou 3 – para carteira 11/17 modalidade Vinculada/Caucionada e carteira 31; 
                // 4 – para carteira 11/17 modalidade Descontada e carteira 51; 
                // 7 – para carteira 17 modalidade Simples.
                if (boleto.ModalidadeCobranca == 0)
                {
                    if (boleto.Carteira.Equals("17-019") || boleto.Carteira.Equals("17-027") || boleto.Carteira.Equals("17-035") || boleto.Carteira.Equals("17-051") || boleto.Carteira.Equals("17-140") || boleto.Carteira.Equals("17-159") || boleto.Carteira.Equals("17-067") || boleto.Carteira.Equals("17-167") || boleto.Carteira.Equals("17"))
                        _segmentoP += "7";
                    else
                        _segmentoP += "0";
                }
                else
                {
                    _segmentoP += boleto.ModalidadeCobranca.ToString();
                }

                // Campo não tratado pelo BB. Forma de cadastramento do título no banco. Pode ser branco/espaço, 0, 1=cobrança registrada, 2=sem registro.
                _segmentoP += "1";
                // Campo não tratado pelo BB. Tipo de documento. Pode ser branco, 0, 1=tradicional, 2=escritural.
                _segmentoP += "2";
                // Campo não tratado pelo BB. Identificação de emissão do boleto. Pode ser branco/espaço, 0, ou:
                // No caso de carteira 11/12/31/51, utilizar código 1 – Banco emite, 4 – Banco reemite, 5 – Banco não reemite, porém nestes dois últimos casos, 
                // o código de Movimento Remessa (posições 16 a 17) deve ser código '31'.
                // Alteração de outros dados (para títulos que já estão registrados no Banco do Brasil). 
                // No caso de carteira 17, podem ser usados os códigos: 1 – Banco emite, 2 – Cliente emite, 3 – Banco pre-emite e cliente complementa, 6 – Cobrança sem papel. 
                // Permite ainda, códigos 4 – Banco reemite e 5 – Banco não reemite, porém o código de Movimento Remessa (posições 16 a 17) deve ser código '31' 
                // Alteração de outros dados (para títulos que já estão registrados no Banco do Brasil). 
                // Obs.: Quando utilizar código, informar de acordo com o que foi cadastrado para a carteira junto ao Banco do Brasil, consulte seu gerente de relacionamento.
                _segmentoP += "2";
                // Campo não tratado pelo BB. Informar 'branco' (espaço) OU zero ou de acordo com a carteira e quem fará a distribuição dos bloquetos. 
                // Para carteira 11/12/31/51 utilizar código 1– Banco distribui. 
                // Para carteira 17, pode ser utilizado código 1 – Banco distribui, 2 – Cliente distribui ou 3 – Banco envia e-mail (nesse caso complementar com registro S), 
                // de acordo com o que foi cadastrado para a carteira junto ao Banco do Brasil, consulte seu gerente de relacionamento.
                _segmentoP += "2";
                _segmentoP += Utils.FitStringLength(boleto.NumeroDocumento, 15, 15, ' ', 0, true, true, false);
                _segmentoP += Utils.FitStringLength(boleto.DataVencimento.ToString("ddMMyyyy"), 8, 8, ' ', 0, true, true, false);
                _segmentoP += Utils.FitStringLength(boleto.ValorBoleto.ToString("f").Replace(",", "").Replace(".", ""), 15, 15, '0', 0, true, true, true);
                _segmentoP += "00000 ";
                _segmentoP += Utils.FitStringLength(boleto.EspecieDocumento.Codigo.ToString(), 2, 2, '0', 0, true, true, true);
                _segmentoP += "N";
                _segmentoP += Utils.FitStringLength(boleto.DataDocumento.ToString("ddMMyyyy"), 8, 8, ' ', 0, true, true, false);

                if (boleto.JurosMora > 0)
                {
                    _segmentoP += "1";
                    _segmentoP += Utils.FitStringLength(boleto.DataVencimento.ToString("ddMMyyyy"), 8, 8, '0', 0, true, true, false);
                    _segmentoP += Utils.FitStringLength(boleto.JurosMora.ApenasNumeros(), 15, 15, '0', 0, true, true, true);
                }
                else if (boleto.JurosPermanente)
                {
                    _segmentoP += "1";
                    _segmentoP += "00000000";
                    _segmentoP += "000000000000000";
                }
                else
                {
                    _segmentoP += "3";
                    _segmentoP += "00000000";
                    _segmentoP += "000000000000000";
                }

                if (boleto.ValorDesconto > 0)
                {
                    _segmentoP += "1";
                    //Alterado por Suélton - 14/07/2017 
                    //Implementação da data limite para o desconto por antecipação
                    _segmentoP +=
                        Utils.FitStringLength(
                            boleto.DataDesconto == DateTime.MinValue
                                ? boleto.DataVencimento.ToString("ddMMyyyy")
                                : boleto.DataDesconto.ToString("ddMMyyyy"), 8, 8, '0', 0, true, true, false);
                    _segmentoP += Utils.FitStringLength(boleto.ValorDesconto.ApenasNumeros(), 15, 15, '0', 0, true, true, true);
                }
                else
                    _segmentoP += "000000000000000000000000";

                _segmentoP += "000000000000000";
                _segmentoP += "000000000000000";
                _segmentoP += Utils.FitStringLength(boleto.NumeroControle ?? boleto.NumeroDocumento, 25, 25, ' ', 0, true, true, false); //alterado por diegodariolli - 15/03/2018

                //alterado por marcelhsouza em 28/03/2013
                //O Banco do Brasil trata somente os códigos '1' – Protestar dias corridos, '2' – Protestar dias úteis, e '3' – Não protestar.
                string codigo_protesto = "3";
                string dias_protesto = "00";

                foreach (IInstrucao instrucao in boleto.Instrucoes)
                {
                    switch ((EnumInstrucoes_BancoBrasil)instrucao.Codigo)
                    {
                        case EnumInstrucoes_BancoBrasil.ProtestarAposNDiasCorridos:
                            codigo_protesto = "1";
                            dias_protesto = Utils.FitStringLength(instrucao.QuantidadeDias.ToString(), 2, 2, '0', 0, true, true, true); //Para código '1' – é possível, de 6 a 29 dias
                            break;
                        case EnumInstrucoes_BancoBrasil.ProtestarAposNDiasUteis:
                            codigo_protesto = "2";
                            dias_protesto = Utils.FitStringLength(instrucao.QuantidadeDias.ToString(), 2, 2, '0', 0, true, true, true); //Para código '2' – é possível, 3º, 4º ou 5º dia útil
                            break;
                        case EnumInstrucoes_BancoBrasil.NaoProtestar:
                            codigo_protesto = "3";
                            dias_protesto = "00";
                            break;
                        default:
                            /*codigo_protesto = "3"; 
                            dias_protesto = "00";*/
                            break;
                    }
                }

                _segmentoP += codigo_protesto;
                _segmentoP += dias_protesto;

                /*if (boleto.Instrucoes.Count > 1 && boleto.Instrucoes[0].QuantidadeDias > 0)
                {
                    _segmentoP += "2";
                    _segmentoP += Utils.FitStringLength(boleto.Instrucoes[0].QuantidadeDias.ToString(), 2, 2, '0', 0, true, true, true);
                }
                else
                    _segmentoP += "300";*/

                //alterado por marcelhsouza em 28/03/2013
                //38.3P Código para Baixa/Devolução 224 224 1 - Numérico C028 Campo não tratado pelo sistema. Informar 'zeros'. O sistema considera a informação que foi cadastrada na sua carteira junto ao Banco do Brasil.
                _segmentoP += "0000090000000000 ";

                //_segmentoP += "2000090000000000 ";

                _segmentoP = Utils.SubstituiCaracteresEspeciais(_segmentoP.ToUpper());

                return _segmentoP;

            }
            catch (Exception ex)
            {
                throw new Exception("Erro durante a geração do SEGMENTO P DO DETALHE do arquivo de REMESSA.", ex);
            }
        }

        public override string GerarDetalheSegmentoQRemessa(Boleto boleto, int numeroRegistro, TipoArquivo tipoArquivo)
        {
            try
            {
                string _zeros16 = new string('0', 16);
                string _brancos28 = new string(' ', 28);
                string _brancos40 = new string(' ', 40);

                string _segmentoQ;

                _segmentoQ = "00100013";
                _segmentoQ += Utils.FitStringLength(numeroRegistro.ToString(), 5, 5, '0', 0, true, true, true);
                _segmentoQ += "Q ";
                _segmentoQ += ObterCodigoDaOcorrencia(boleto);

                if (boleto.Sacado.CPFCNPJ.Length <= 11)
                    _segmentoQ += "1";
                else
                    _segmentoQ += "2";

                var enderecoSacadoComNumero = boleto.Sacado.Endereco.End;
                if (!string.IsNullOrEmpty(boleto.Sacado.Endereco.Numero))
                {
                    enderecoSacadoComNumero += ", " + boleto.Sacado.Endereco.Numero;
                }

                _segmentoQ += Utils.FitStringLength(boleto.Sacado.CPFCNPJ, 15, 15, '0', 0, true, true, true);
                _segmentoQ += Utils.FitStringLength(boleto.Sacado.Nome.TrimStart(' '), 40, 40, ' ', 0, true, true, false).ToUpper();
                _segmentoQ += Utils.FitStringLength(enderecoSacadoComNumero.TrimStart(' '), 40, 40, ' ', 0, true, true, false).ToUpper();
                _segmentoQ += Utils.FitStringLength(boleto.Sacado.Endereco.Bairro.TrimStart(' '), 15, 15, ' ', 0, true, true, false).ToUpper();
                _segmentoQ += Utils.FitStringLength(boleto.Sacado.Endereco.CEP, 8, 8, ' ', 0, true, true, false).ToUpper(); ;
                _segmentoQ += Utils.FitStringLength(boleto.Sacado.Endereco.Cidade.TrimStart(' '), 15, 15, ' ', 0, true, true, false).ToUpper();
                _segmentoQ += Utils.FitStringLength(boleto.Sacado.Endereco.UF, 2, 2, ' ', 0, true, true, false).ToUpper();
                _segmentoQ += _zeros16;
                _segmentoQ += _brancos40;
                _segmentoQ += "000";
                _segmentoQ += _brancos28;

                _segmentoQ = Utils.SubstituiCaracteresEspeciais(_segmentoQ);

                return _segmentoQ;

            }
            catch (Exception ex)
            {
                throw new Exception("Erro durante a geração do SEGMENTO Q DO DETALHE do arquivo de REMESSA.", ex);
            }
        }
        public override string GerarDetalheSegmentoRRemessa(Boleto boleto, int numeroRegistro, TipoArquivo tipoArquivo)
        {
            try
            {
                string _brancos110 = new string(' ', 110);
                string _brancos9 = new string(' ', 9);

                string _segmentoR;

                _segmentoR = "00100013";
                _segmentoR += Utils.FitStringLength(numeroRegistro.ToString(), 5, 5, '0', 0, true, true, true);
                _segmentoR += "R ";
                _segmentoR += ObterCodigoDaOcorrencia(boleto);

                //Suelton - 14/12/2018 - Implementação do 2 desconto por antecipação
                if (boleto.DataDescontoAntecipacao2.HasValue && boleto.ValorDescontoAntecipacao2.HasValue)
                {
                    _segmentoR += "1" + //'1' = Valor Fixo Até a Data Informada
                        Utils.FitStringLength(boleto.DataDescontoAntecipacao2.Value.ToString("ddMMyyyy"), 8, 8, '0', 0, true, true, false) +
                        Utils.FitStringLength(boleto.ValorDescontoAntecipacao2.ApenasNumeros(), 15, 15, '0', 0, true, true, true);
                }
                else
                {
                    // Desconto 2
                    _segmentoR += "000000000000000000000000"; //24 zeros
                }

                //Suelton - 14/12/2018 - Implementação do 3 desconto por antecipação
                if (boleto.DataDescontoAntecipacao3.HasValue && boleto.ValorDescontoAntecipacao3.HasValue)
                {
                    _segmentoR += "1" + //'1' = Valor Fixo Até a Data Informada
                        Utils.FitStringLength(boleto.DataDescontoAntecipacao3.Value.ToString("ddMMyyyy"), 8, 8, '0', 0, true, true, false) +
                        Utils.FitStringLength(boleto.ValorDescontoAntecipacao3.ApenasNumeros(), 15, 15, '0', 0, true, true, true);
                }
                else
                {
                    // Desconto 3
                    _segmentoR += "000000000000000000000000"; //24 zeros
                }


                if (boleto.PercMulta > 0)
                {
                    // Código da multa 2 - percentual
                    _segmentoR += "2";
                }
                else if (boleto.ValorMulta > 0)
                {
                    // Código da multa 1 - valor fixo
                    _segmentoR += "1";
                }
                else
                {
                    // Código da multa 0 - sem multa
                    _segmentoR += "0";
                }

                _segmentoR += Utils.FitStringLength(boleto.DataMulta.ToString("ddMMyyyy"), 8, 8, '0', 0, true, true, false);

                // Multa em Percentual (%), Valor (R$)
                if (boleto.PercMulta > 0)
                {
                    _segmentoR += Utils.FitStringLength(boleto.PercMulta.ApenasNumeros(), 15, 15, '0', 0, true, true, true);
                }
                else
                {
                    _segmentoR += Utils.FitStringLength(boleto.ValorMulta.ApenasNumeros(), 15, 15, '0', 0, true, true, true);
                }

                _segmentoR += _brancos110;
                _segmentoR += "0000000000000000"; //16 zeros
                _segmentoR += " "; //1 branco
                _segmentoR += "000000000000"; //12 zeros
                _segmentoR += "  "; //2 brancos
                _segmentoR += "0"; //1 zero
                _segmentoR += _brancos9;

                _segmentoR = Utils.SubstituiCaracteresEspeciais(_segmentoR);

                return _segmentoR;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro durante a geração do SEGMENTO R DO DETALHE do arquivo de REMESSA.", ex);
            }
        }
        /// <summary>
        /// TRAILER do arquivo CNAB
        /// Gera o TRAILER do arquivo remessa de acordo com o lay-out informado
        /// </summary>
        public override string GerarTrailerRemessa(int numeroRegistro, TipoArquivo tipoArquivo, Cedente cedente, decimal vltitulostotal)
        {
            try
            {
                string _trailer = " ";

                base.GerarTrailerRemessa(numeroRegistro, tipoArquivo, cedente, vltitulostotal);

                switch (tipoArquivo)
                {
                    case TipoArquivo.CNAB240:
                        _trailer = GerarTrailerRemessa240();
                        break;
                    case TipoArquivo.CNAB400:
                        _trailer = GerarTrailerRemessa400(numeroRegistro, 0);
                        break;
                    case TipoArquivo.Outro:
                        throw new Exception("Tipo de arquivo inexistente.");
                }

                return _trailer;

            }
            catch (Exception ex)
            {
                throw new Exception("Erro durante a geração do TRAILER do arquivo de REMESSA.", ex);
            }
        }
        public override string GerarTrailerLoteRemessa(int numeroRegistro)
        {
            try
            {
                string trailer = Utils.FormatCode(Codigo.ToString(), "0", 3, true);
                trailer += Utils.FitStringLength("1", 4, 4, '0', 0, true, true, true);
                trailer += "5";
                trailer += Utils.FormatCode("", " ", 9);
                trailer += Utils.FitStringLength(numeroRegistro.ToString(), 6, 6, '0', 0, true, true, true);  //posição 18 até 23   (6) - Quantidade de Registros no Lote
                trailer += Utils.FormatCode("", "0", 92, true);
                trailer += Utils.FormatCode("", " ", 125);
                trailer = Utils.SubstituiCaracteresEspeciais(trailer);
                return trailer;
            }
            catch (Exception e)
            {
                throw new Exception("Erro durante a geração do registro TRAILER do LOTE de REMESSA.", e);
            }
        }

        public override string GerarTrailerArquivoRemessa(int numeroRegistro)
        {
            try
            {
                string _brancos205 = new string(' ', 205);

                string _trailerArquivo;

                _trailerArquivo = "00199999         000001";
                _trailerArquivo += Utils.FitStringLength((numeroRegistro).ToString(), 6, 6, '0', 0, true, true, true);
                _trailerArquivo += "000000";
                _trailerArquivo += _brancos205;

                _trailerArquivo = Utils.SubstituiCaracteresEspeciais(_trailerArquivo);

                return _trailerArquivo;
            }
            catch (Exception ex)
            {
                throw new Exception("", ex);
            }
        }

        public override string GerarHeaderRemessa(string numeroConvenio, Cedente cedente, TipoArquivo tipoArquivo, int numeroArquivoRemessa, Boleto boletos)
        {
            throw new NotImplementedException("Função não implementada.");
        }
        #endregion

        #region CNAB240 - Específicos
        public bool ValidarRemessaCNAB240(string numeroConvenio, IBanco banco, Cedente cedente, Boletos boletos, int numeroArquivoRemessa, out string mensagem)
        {
            string vMsg = string.Empty;
            mensagem = vMsg;
            return true;
            //throw new NotImplementedException("Função não implementada.");
        }
        private string GerarHeaderLoteRemessaCNAB240(string numeroConvenio, Cedente cedente, int numeroArquivoRemessa)
        {
            try
            {
                string _brancos40 = new string(' ', 40);
                string _brancos33 = new string(' ', 33);
                string _headerLote;

                //alterado por marcelhsouza em 28/03/2013
                //_headerLote = "00100011R0100";
                _headerLote = "00100011R01  "; //posições 12-13 são espaços e não zeros
                // Campo não criticado pelo sistema, informar ZEROS ou nº da versão do layout do arquivo que foi utilizado
                // para a formatação dos campos.
                // Como não sei onde pegar esse nº, deixei como padrão.
                //alterado por marcelhsouza em 28/03/2013
                //_headerLote += "020";
                _headerLote += "042"; //no header do arquivo esta 000, entao aqui deve-se por 000, poderia ser 020 SE no header do arquivo tivesse 030
                _headerLote += " ";

                if (cedente.CPFCNPJ.Length <= 11)
                    _headerLote += "1";
                else
                    _headerLote += "2";

                _headerLote += Utils.FitStringLength(cedente.CPFCNPJ, 15, 15, '0', 0, true, true, true);
                _headerLote += Utils.FitStringLength(numeroConvenio, 9, 9, '0', 0, true, true, true);
                _headerLote += "0014";
                // O Código da carteira é dividida em 2 partes:
                // - nº da carteira 9(02)
                // - variação (se houver) 9(03)
                if (cedente.Carteira.Length == 2)
                    _headerLote += cedente.Carteira.ToString() + "019  ";
                else
                    _headerLote += cedente.Carteira.Replace("-", "") + "  ";

                _headerLote += Utils.FitStringLength(cedente.ContaBancaria.Agencia, 5, 5, '0', 0, true, true, true);
                _headerLote += Utils.FitStringLength(cedente.ContaBancaria.DigitoAgencia, 1, 1, '0', 0, true, true, true);
                _headerLote += Utils.FitStringLength(cedente.ContaBancaria.Conta, 12, 12, '0', 0, true, true, true);
                _headerLote += Utils.FitStringLength(cedente.ContaBancaria.DigitoConta, 1, 1, '0', 0, true, true, true);
                // Dígito verificador  da Agência/Conta.  Campo não tratado pelo BB.  Informar ESPAÇO ou ZERO.
                _headerLote += " "; // jefhtavares O banco não aceita mais esse campo como 0 (zero), o campo deverá ser enviado em branco
                _headerLote += Utils.FitStringLength(cedente.Nome, 30, 30, ' ', 0, true, true, false);
                _headerLote += _brancos40;
                _headerLote += _brancos40;
                // Campo não tratado pelo BB. Sugerem utilizar nº sequencial para controle da empresa.  Não especifica se é controle de arquivo.
                // Em todo caso, coloquei o nº sequencial do arquivo remessa.
                _headerLote += Utils.FitStringLength(numeroArquivoRemessa.ToString(), 8, 8, '0', 0, true, true, true);
                _headerLote += DateTime.Now.ToString("ddMMyyyy");
                _headerLote += "00000000";
                _headerLote += _brancos33;

                _headerLote = Utils.SubstituiCaracteresEspeciais(_headerLote.ToUpper());

                return _headerLote;

            }
            catch (Exception ex)
            {
                throw new Exception("Erro durante a geração do HEADER DE LOTE do arquivo de REMESSA.", ex);
            }
        }
        public string GerarHeaderRemessaCNAB240(Cedente cedente, int numeroArquivoRemessa)
        {
            try
            {
                string _brancos20 = new string(' ', 20);
                string _brancos10 = new string(' ', 10);
                string _header;

                _header = "00100000         ";
                if (cedente.CPFCNPJ.Length <= 11)
                    _header += "1";
                else
                    _header += "2";
                _header += Utils.FitStringLength(cedente.CPFCNPJ, 14, 14, '0', 0, true, true, true);
                _header += Utils.FitStringLength(cedente.Convenio.ToString(), 9, 9, '0', 0, true, true, true);
                _header += "0014";
                // adicionado por Heric Souza em 02/06/2017
                if (cedente.Carteira.Length == 2)
                    _header += cedente.Carteira.ToString() + "019";
                else
                    _header += Utils.FitStringLength(cedente.Carteira.Replace("-", ""), 5, 5, ' ', 0, true, true, false);
                //_header += Utils.FitStringLength(cedente.Carteira, 2, 2, '0', 0, true, true, true);
                //_header += "019";
                _header += "  ";
                _header += Utils.FitStringLength(cedente.ContaBancaria.Agencia, 5, 5, '0', 0, true, true, true);
                _header += Utils.FitStringLength(cedente.ContaBancaria.DigitoAgencia, 1, 1, ' ', 0, true, true, false);
                _header += Utils.FitStringLength(cedente.ContaBancaria.Conta, 12, 12, '0', 0, true, true, true);
                _header += Utils.FitStringLength(cedente.ContaBancaria.DigitoConta, 1, 1, ' ', 0, true, true, false);
                _header += "0"; // DÍGITO VERIFICADOR DA AG./CONTA
                _header += Utils.FitStringLength(cedente.Nome, 30, 30, ' ', 0, true, true, false);
                _header += Utils.FitStringLength("BANCO DO BRASIL", 30, 30, ' ', 0, true, true, false);
                _header += _brancos10;
                _header += "1";
                _header += DateTime.Now.ToString("ddMMyyyy");
                _header += DateTime.Now.ToString("HHmmss"); //alterado por diegodariolli - 15/03/2018
                // NÚMERO SEQUENCIAL DO ARQUIVO *EVOLUIR UM NÚMERO A CADA HEADER DE ARQUIVO
                //_header += "000001";
                //alterado por MarcelHenrique 13/04/2013 deve-se ser sequencia a numeracao do arquivo incrementando a cada envio
                _header += Utils.FitStringLength(numeroArquivoRemessa.ToString(), 6, 6, '0', 0, true, true, true);
                // Campo não criticado pelo sistema, informar ZEROS ou nº da versão do layout do arquivo que foi utilizado
                // para a formatação dos campos.
                // Como não sei onde pegar esse nº, deixei como padrão.
                _header += "050";
                _header += "00000";
                _header += _brancos20;
                _header += _brancos20;
                _header += _brancos10;
                _header += "    ";
                _header += "     ";
                _header += _brancos10;

                _header = Utils.SubstituiCaracteresEspeciais(_header.ToUpper());

                return _header;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro durante a geração do HEADER DE ARQUIVO do arquivo de REMESSA.", ex);
            }
        }
        public string GerarDetalheRemessaCNAB240(Boleto boleto, int numeroRegistro, TipoArquivo tipoArquivo)
        {
            throw new NotImplementedException("Função não implementada.");
        }
        public string GerarTrailerRemessa240()
        {
            throw new NotImplementedException("Função não implementada.");
        }
        public override DetalheSegmentoTRetornoCNAB240 LerDetalheSegmentoTRetornoCNAB240(string registro)
        {

            string _Controle_numBanco = registro.Substring(0, 3); //01
            string _Controle_lote = registro.Substring(3, 7); //02
            string _Controle_regis = registro.Substring(7, 1); //03
            string _Servico_numRegis = registro.Substring(8, 5); //04
            string _Servico_segmento = registro.Substring(13, 1); //05
            string _cnab06 = registro.Substring(14, 1); //06
            string _Servico_codMov = registro.Substring(15, 2); //07
            string _ccAgenciaCodigo = registro.Substring(17, 5); //08
            string _ccAgenciaDv = registro.Substring(22, 1); //09
            string _ccContaNumero = registro.Substring(23, 12); //10
            string _ccContaDv = registro.Substring(35, 1); //11
            string _ccDv = registro.Substring(36, 1); //12
            string _outUsoExclusivo = registro.Substring(37, 9); //13
            string _outNossoNumero = registro.Substring(37, 20); //14
            string _outCarteira = registro.Substring(57, 1); //15
            string _outNumeroDocumento = registro.Substring(58, 15); //16
            string _outVencimento = registro.Substring(73, 8); //17
            string _outValor = registro.Substring(81, 15); //18
            string _outUf = registro.Substring(96, 2); //19
            string _outBanco = registro.Substring(98, 1); //20
            string _outCodCedente = registro.Substring(99, 5); //21
            string _outDvCedente = registro.Substring(104, 1); //22
            string _outNomeCedente = registro.Substring(105, 25); //23
            string _outCodMoeda = registro.Substring(130, 2); //24
            string _sacadoInscricaoTipo = registro.Substring(132, 1); //25
            string _sacadoInscricaoNumero = registro.Substring(133, 15); //26
            string _sacadoNome = registro.Substring(148, 40); //27
            string _cnab28 = registro.Substring(188, 10); //28
            string _valorTarifasCustas = registro.Substring(198, 13); //29
            string _motivoCobraca = registro.Substring(213, 10); //30
            string _cnab31 = registro.Substring(223, 17); //31

            try
            {
                /* 05 */
                if (!registro.Substring(13, 1).Equals(@"T"))
                {
                    throw new Exception("Registro inválida. O detalhe não possuí as características do segmento T.");
                }
                DetalheSegmentoTRetornoCNAB240 segmentoT = new DetalheSegmentoTRetornoCNAB240(registro);
                segmentoT.CodigoBanco = Convert.ToInt32(registro.Substring(0, 3)); //01
                segmentoT.idCodigoMovimento = Convert.ToInt32(registro.Substring(15, 2)); //07
                segmentoT.Agencia = Convert.ToInt32(registro.Substring(17, 5)); //08
                segmentoT.DigitoAgencia = registro.Substring(22, 1); //09
                segmentoT.Conta = Convert.ToInt64(registro.Substring(23, 12)); //10
                segmentoT.DigitoConta = registro.Substring(35, 1); //11
                segmentoT.DACAgenciaConta = (string.IsNullOrEmpty(registro.Substring(36, 1).Trim())) ? 0 : Convert.ToInt32(registro.Substring(36, 1)); //12
                segmentoT.NossoNumero = registro.Substring(37, 20); //14
                segmentoT.CodigoCarteira = Convert.ToInt32(registro.Substring(57, 1)); //15
                segmentoT.NumeroDocumento = registro.Substring(58, 15); //16
                segmentoT.DataVencimento = registro.Substring(73, 8).ToString() == "00000000" ? DateTime.Now : DateTime.ParseExact(registro.Substring(73, 8), "ddMMyyyy", CultureInfo.InvariantCulture); //17
                segmentoT.ValorTitulo = Convert.ToDecimal(registro.Substring(81, 15)) / 100; //18
                segmentoT.IdentificacaoTituloEmpresa = registro.Substring(105, 25); //23
                segmentoT.TipoInscricao = Convert.ToInt32(registro.Substring(132, 1)); //25
                segmentoT.NumeroInscricao = registro.Substring(133, 15); //26
                segmentoT.NomeSacado = registro.Substring(148, 40); //27
                segmentoT.ValorTarifas = Convert.ToDecimal(registro.Substring(198, 15)) / 100; //29
                //Jéferson (jefhtavares) em 12/12/2013 - O campo Valor Tarifas é composto de 15 posições (199-213) e não de 13
                segmentoT.CodigoRejeicao = registro.Substring(213, 1) == "A" ? registro.Substring(214, 9) : registro.Substring(213, 10); //30
                segmentoT.UsoFebraban = _cnab31;

                return segmentoT;
            }
            catch (Exception ex)
            {
                //TrataErros.Tratar(ex);
                throw new Exception("Erro ao processar arquivo de RETORNO - SEGMENTO T.", ex);
            }
        }
        public override DetalheSegmentoURetornoCNAB240 LerDetalheSegmentoURetornoCNAB240(string registro)
        {
            string _Controle_numBanco = registro.Substring(0, 3); //01
            string _Controle_lote = registro.Substring(3, 7); //02
            string _Controle_regis = registro.Substring(7, 1); //03
            string _Servico_numRegis = registro.Substring(8, 5); //04
            string _Servico_segmento = registro.Substring(13, 1); //05
            string _cnab06 = registro.Substring(14, 1); //06
            string _Servico_codMov = registro.Substring(15, 2); //07
            string _dadosTituloAcrescimo = registro.Substring(17, 15); //08
            string _dadosTituloValorDesconto = registro.Substring(32, 15); //09
            string _dadosTituloValorAbatimento = registro.Substring(47, 15); //10
            string _dadosTituloValorIof = registro.Substring(62, 15); //11
            string _dadosTituloValorPago = registro.Substring(76, 15); //12
            string _dadosTituloValorCreditoBruto = registro.Substring(92, 15); //13
            string _outDespesas = registro.Substring(107, 15); //14
            string _outPerCredEntRecb = registro.Substring(122, 5); //15
            string _outBanco = registro.Substring(127, 10); //16
            string _outDataOcorrencia = registro.Substring(137, 8); //17
            string _outDataCredito = registro.Substring(145, 8); //18
            string _cnab19 = registro.Substring(153, 87); //19


            try
            {

                if (!registro.Substring(13, 1).Equals(@"U"))
                {
                    throw new Exception("Registro inválida. O detalhe não possuí as características do segmento U.");
                }

                var segmentoU = new DetalheSegmentoURetornoCNAB240(registro);
                segmentoU.Servico_Codigo_Movimento_Retorno = Convert.ToDecimal(registro.Substring(15, 2)); //07.3U|Serviço|Cód. Mov.|Código de Movimento Retorno
                segmentoU.JurosMultaEncargos = Convert.ToDecimal(registro.Substring(17, 15)) / 100;
                segmentoU.ValorDescontoConcedido = Convert.ToDecimal(registro.Substring(32, 15)) / 100;
                segmentoU.ValorAbatimentoConcedido = Convert.ToDecimal(registro.Substring(47, 15)) / 100;
                segmentoU.ValorIOFRecolhido = Convert.ToDecimal(registro.Substring(62, 15)) / 100;
                segmentoU.ValorOcorrenciaSacado = segmentoU.ValorPagoPeloSacado = Convert.ToDecimal(registro.Substring(77, 15)) / 100;
                segmentoU.ValorLiquidoASerCreditado = Convert.ToDecimal(registro.Substring(92, 15)) / 100;
                segmentoU.ValorOutrasDespesas = Convert.ToDecimal(registro.Substring(107, 15)) / 100;
                segmentoU.ValorOutrosCreditos = Convert.ToDecimal(registro.Substring(122, 15)) / 100;
                segmentoU.DataOcorrencia = segmentoU.DataOcorrencia = DateTime.ParseExact(registro.Substring(137, 8), "ddMMyyyy", CultureInfo.InvariantCulture);
                segmentoU.DataCredito = registro.Substring(145, 8).ToString() == "00000000" ? segmentoU.DataOcorrencia : DateTime.ParseExact(registro.Substring(145, 8), "ddMMyyyy", CultureInfo.InvariantCulture);
                segmentoU.CodigoOcorrenciaSacado = registro.Substring(15, 2);

                return segmentoU;
            }
            catch (Exception ex)
            {
                //TrataErros.Tratar(ex);
                throw new Exception("Erro ao processar arquivo de RETORNO - SEGMENTO U.", ex);
            }
        }
        #endregion

        internal static string Mod11BancoBrasil(string value)
        {
            #region Trecho do manual DVMD11.doc
            /* 
            Multiplicar cada algarismo que compõe o número pelo seu respectivo multiplicador (PESO).
            Os multiplicadores(PESOS) variam de 9 a 2.
            O primeiro dígito da direita para a esquerda deverá ser multiplicado por 9, o segundo por 8 e assim sucessivamente.
            O resultados das multiplicações devem ser somados:
            72+35+24+27+4+9+8=179
            O total da soma deverá ser dividido por 11:
            179 / 11=16
            RESTO=3
            Se o resto da divisão for igual a 10 o D.V. será igual a X. 
            Se o resto da divisão for igual a 0 o D.V. será igual a 0.
            Se o resto for menor que 10, o D.V.  será igual ao resto.
            No exemplo acima, o dígito verificador será igual a 3
            */
            #endregion

            /* d - Dígito
             * s - Soma
             * p - Peso
             * b - Base
             * r - Resto
             */

            string d;
            int s = 0, p = 9, b = 2;

            for (int i = value.Length - 1; i >= 0; i--)
            {
                s += (int.Parse(value[i].ToString()) * p);
                if (p == b)
                    p = 9;
                else
                    p--;
            }

            int r = (s % 11);
            if (r == 10)
                d = "X";
            else if (r == 0)
                d = "0";
            else
                d = r.ToString();

            return d;
        }

        #region Métodos de processamento do arquivo retorno CNAB400


        #endregion

        #region CNAB 400 - Específicos sidneiklein

        public bool ValidarRemessaCNAB400(string numeroConvenio, IBanco banco, Cedente cedente, Boletos boletos, int numeroArquivoRemessa, out string mensagem)
        {
            bool vRetorno = true;
            string vMsg = string.Empty;
            //
            #region Pré Validações
            if (banco == null)
            {
                vMsg += string.Concat("Remessa: O Banco é Obrigatório!", Environment.NewLine);
                vRetorno = false;
            }
            if (cedente == null)
            {
                vMsg += string.Concat("Remessa: O Cedente/Beneficiário é Obrigatório!", Environment.NewLine);
                vRetorno = false;
            }
            if (boletos == null || boletos.Count.Equals(0))
            {
                vMsg += string.Concat("Remessa: Deverá existir ao menos 1 boleto para geração da remessa!", Environment.NewLine);
                vRetorno = false;
            }
            #endregion
            //
            foreach (Boleto boleto in boletos)
            {
                #region Validação de cada boleto
                if (boleto.Remessa == null)
                {
                    vMsg += string.Concat("Boleto: ", boleto.NumeroDocumento, "; Remessa: Informe as diretrizes de remessa!", Environment.NewLine);
                    vRetorno = false;
                }
                else
                {
                    #region Validações da Remessa que deverão estar preenchidas quando BANCO DO BRASIL
                    if (string.IsNullOrEmpty(boleto.Remessa.TipoDocumento))
                    {
                        vMsg += string.Concat("Boleto: ", boleto.NumeroDocumento, "; Remessa: Informe o Tipo Documento!", Environment.NewLine);
                        vRetorno = false;
                    }
                    #endregion
                }
                #endregion
            }
            //
            mensagem = vMsg;
            return vRetorno;
        }

        public string GerarHeaderRemessaCNAB400(Cedente cedente, int numeroArquivoRemessa)
        {
            try
            {
                TRegistroEDI reg = new TRegistroEDI();
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediNumericoSemSeparador_, 0001, 001, 0, "0", '0'));                                   //001-001
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediNumericoSemSeparador_, 0002, 001, 0, "1", '0'));                                   //002-002
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0003, 007, 0, "REMESSA", ' '));                             //003-009 "TESTE"
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediNumericoSemSeparador_, 0010, 002, 0, "01", '0'));                                  //010-011
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0012, 008, 0, "COBRANCA", ' '));                            //012-019
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0020, 007, 0, string.Empty, ' '));                          //020-026
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediNumericoSemSeparador_, 0027, 004, 0, cedente.ContaBancaria.Agencia, '0'));         //027-030
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0031, 001, 0, cedente.ContaBancaria.DigitoAgencia, ' '));   //031-031
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediNumericoSemSeparador_, 0032, 008, 0, cedente.ContaBancaria.Conta, '0'));           //032-039
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0040, 001, 0, cedente.ContaBancaria.DigitoConta, ' '));     //040-040
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediNumericoSemSeparador_, 0041, 006, 0, "000000", '0'));                              //041-046
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0047, 030, 0, cedente.Nome.ToUpper(), ' '));                //047-076
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0077, 018, 0, "001BANCODOBRASIL", ' '));                    //077-094
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediDataDDMMAA___________, 0095, 006, 0, DateTime.Now, ' '));                          //095-100
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediNumericoSemSeparador_, 0101, 007, 0, numeroArquivoRemessa, '0'));                  //101-107
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0108, 022, 0, string.Empty, ' '));                          //108-129
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediNumericoSemSeparador_, 0130, 007, 0, cedente.Convenio, '0'));                      //130-136
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0137, 258, 0, string.Empty, ' '));                          //137-394
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0395, 006, 0, "000001", ' '));                              //395-400
                //
                reg.CodificarLinha();
                //
                string vLinha = reg.LinhaRegistro;
                string _header = Utils.SubstituiCaracteresEspeciais(vLinha);
                //
                return _header;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao gerar HEADER do arquivo de remessa do CNAB400.", ex);
            }
        }

        public string GerarDetalheRemessaCNAB400(Boleto boleto, int numeroRegistro, TipoArquivo tipoArquivo)
        {
            try
            {
                #region Regra Tipo de Inscrição Cedente

                string vCpfCnpjEmi = "00";
                if (boleto.Cedente.CPFCNPJ.Length.Equals(11)) vCpfCnpjEmi = "01"; //Cpf é sempre 11;
                else if (boleto.Cedente.CPFCNPJ.Length.Equals(14)) vCpfCnpjEmi = "02"; //Cnpj é sempre 14;

                #endregion

                #region Instruções
                string vInstrucao1 = "0";
                string vInstrucao2 = "0";
                string diasProtesto = string.Empty;
                switch (boleto.Instrucoes.Count)
                {
                    case 1:
                        vInstrucao1 = boleto.Instrucoes[0].Codigo.ToString();
                        vInstrucao2 = "0";
                        diasProtesto = boleto.Instrucoes[0].QuantidadeDias.ToString().PadLeft(2, '0');
                        break;
                    case 2:
                        vInstrucao1 = boleto.Instrucoes[0].Codigo.ToString();
                        vInstrucao2 = boleto.Instrucoes[1].Codigo.ToString();
                        diasProtesto = boleto.Instrucoes[0].QuantidadeDias.ToString().PadLeft(2, '0');
                        break;
                    case 3:
                        vInstrucao1 = boleto.Instrucoes[0].Codigo.ToString();
                        vInstrucao2 = boleto.Instrucoes[1].Codigo.ToString();
                        diasProtesto = boleto.Instrucoes[0].QuantidadeDias.ToString().PadLeft(2, '0');
                        break;
                }
                #endregion

                #region Carteira

                boleto.Cedente.Carteira = boleto.Cedente.Carteira.Replace("-", string.Empty);
                if (boleto.Cedente.Carteira.Length > 2)
                {
                    boleto.VariacaoCarteira = boleto.Cedente.Carteira.Substring(2, 3);
                    boleto.Carteira = boleto.Cedente.Carteira.Substring(0, 2);
                }

                #endregion

                base.GerarDetalheRemessa(boleto, numeroRegistro, tipoArquivo);

                TRegistroEDI reg = new TRegistroEDI();
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediNumericoSemSeparador_, 0001, 001, 0, "7", '0'));                                       //001-001
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediNumericoSemSeparador_, 0002, 002, 0, vCpfCnpjEmi, '0'));                               //002-003
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediNumericoSemSeparador_, 0004, 014, 0, boleto.Cedente.CPFCNPJ, '0'));                    //004-017
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediNumericoSemSeparador_, 0018, 004, 0, boleto.Cedente.ContaBancaria.Agencia, '0'));      //018-021
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0022, 001, 0, boleto.Cedente.ContaBancaria.DigitoAgencia, ' '));//022-022
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediNumericoSemSeparador_, 0023, 008, 0, boleto.Cedente.ContaBancaria.Conta, '0'));        //023-030
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0031, 001, 0, boleto.Cedente.ContaBancaria.DigitoConta, ' '));  //031-031
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0032, 007, 0, boleto.Cedente.Convenio, ' '));                   //032-038
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0039, 025, 0, boleto.NumeroControle, ' '));                     //039-063 //alterado por diegodariolli 15/03/2018
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediNumericoSemSeparador_, 0064, 017, 0, boleto.NossoNumero, '0'));                        //064-080
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediNumericoSemSeparador_, 0081, 002, 0, "00", '0'));                                      //081-082
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediNumericoSemSeparador_, 0083, 002, 0, "00", '0'));                                      //083-084
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0085, 003, 0, string.Empty, ' '));                              //085-087
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0088, 001, 0, string.Empty, ' '));                              //088-088
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0089, 003, 0, string.Empty, ' '));                              //089-091
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediNumericoSemSeparador_, 0092, 003, 0, boleto.VariacaoCarteira, '0'));                   //092-094
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediNumericoSemSeparador_, 0095, 001, 0, "0", '0'));                                       //095-095
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediNumericoSemSeparador_, 0096, 006, 0, "0", '0'));                                       //096-101
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0102, 005, 0, boleto.TipoDeCobranca, ' '));                     //102-106
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediNumericoSemSeparador_, 0107, 002, 0, boleto.Carteira, '0'));                           //107-108
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0109, 002, 0, ObterCodigoDaOcorrencia(boleto), ' '));           //109-110
                var numeroControle = boleto.NumeroControle ?? boleto.NumeroDocumento;
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0111, 010, 0, numeroControle, '0'));                            //111-120
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediDataDDMMAA___________, 0121, 006, 0, boleto.DataVencimento, ' '));                     //121-126
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediNumericoSemSeparador_, 0127, 013, 2, boleto.ValorBoleto, '0'));                        //127-139
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0140, 003, 0, "001", '0'));                                     //140-142   
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediNumericoSemSeparador_, 0143, 004, 0, "0000", '0'));                                    //143-146
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0147, 001, 0, string.Empty, ' '));                              //147-147 

                string especie = boleto.Especie;
                if (boleto.EspecieDocumento.Sigla == "DM") // Conforme nota 7 explicativa do banco
                    especie = "01";

                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediNumericoSemSeparador_, 0148, 002, 0, especie, '0'));                                   //148-149
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0150, 001, 0, boleto.Aceite, ' '));                             //150-150
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediDataDDMMAA___________, 0151, 006, 0, boleto.DataProcessamento, ' '));                  //151-156
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediNumericoSemSeparador_, 0157, 002, 0, vInstrucao1, '0'));                               //157-158
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediNumericoSemSeparador_, 0159, 002, 0, vInstrucao2, '0'));                               //159-160
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediNumericoSemSeparador_, 0161, 013, 2, boleto.JurosMora, '0'));                          //161-173

                #region Instruções Conforme Código de Ocorrência...
                if (boleto.Remessa.CodigoOcorrencia.Equals("35") || boleto.Remessa.CodigoOcorrencia.Equals("36"))   //“35” – Cobrar Multa – ou “36” - Dispensar Multa 
                {
                    #region Código de Multa e Valor/Percentual Multa
                    string vCodigoMulta = "9"; //“9” = Dispensar Multa
                    decimal vMulta = 0;

                    if (boleto.ValorMulta > 0)
                    {
                        vCodigoMulta = "1";    //“1” = Valor
                        vMulta = boleto.ValorMulta;
                    }
                    else if (boleto.PercMulta > 0)
                    {
                        vCodigoMulta = "2";   //“2” = Percentual
                        vMulta = boleto.PercMulta;
                    }
                    #endregion

                    #region DataVencimento
                    string vDataVencimento;
                    if (!boleto.DataVencimento.Equals(DateTime.MinValue))
                        vDataVencimento = boleto.DataVencimento.ToString("dd/MM/yy");
                    else
                        throw new Exception("Data de inicio para Cobrança da Multa inválida.");
                    #endregion

                    reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediNumericoSemSeparador_, 0174, 001, 0, vCodigoMulta, '0'));                          //174 a 174      Código da Multa 1=Valor 
                    reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediDataDDMMAA___________, 0175, 006, 0, DateTime.Parse(vDataVencimento), ' '));       //175 a 180      Data de inicio para Cobrança da Multa 
                    reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediNumericoSemSeparador_, 0180, 013, 2, vMulta, '0'));                                //181 a 192      Valor de Multa 
                }
                else
                {
                    #region DataDesconto
                    string vDataDesconto = "000000";
                    if (!boleto.DataDesconto.Equals(DateTime.MinValue))
                        vDataDesconto = boleto.DataDesconto.ToString("ddMMyy");
                    #endregion
                    reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediNumericoSemSeparador_, 0174, 006, 0, vDataDesconto, '0'));                             //174-179                
                    reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediNumericoSemSeparador_, 0180, 013, 2, boleto.ValorDesconto, '0'));                      //180-192                
                    reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediNumericoSemSeparador_, 0193, 013, 2, boleto.IOF, '0'));                                //193-205
                }
                #endregion

                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediNumericoSemSeparador_, 0206, 013, 2, boleto.Abatimento, '0'));                         //206-218

                #region Regra Tipo de Inscrição Sacado
                string vCpfCnpjSac = "00";
                if (boleto.Sacado.CPFCNPJ.Length.Equals(11)) vCpfCnpjSac = "01"; //Cpf é sempre 11;
                else if (boleto.Sacado.CPFCNPJ.Length.Equals(14)) vCpfCnpjSac = "02"; //Cnpj é sempre 14;
                #endregion

                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediNumericoSemSeparador_, 0219, 002, 0, vCpfCnpjSac, '0'));                               //219-220
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediNumericoSemSeparador_, 0221, 014, 0, boleto.Sacado.CPFCNPJ, '0'));                     //221-234
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0235, 037, 0, boleto.Sacado.Nome.ToUpper(), ' '));              //235-271
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0272, 003, 0, string.Empty, ' '));                              //272-274

                var enderecoSacadoComNumero = boleto.Sacado.Endereco.End;
                if (!string.IsNullOrEmpty(boleto.Sacado.Endereco.Numero))
                {
                    enderecoSacadoComNumero += ", " + boleto.Sacado.Endereco.Numero;
                }

                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0275, 040, 0, enderecoSacadoComNumero.ToUpper(), ' '));         //275-314               
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0315, 012, 0, boleto.Sacado.Endereco.Bairro.ToUpper(), ' '));   //315-326
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediNumericoSemSeparador_, 0327, 008, 0, boleto.Sacado.Endereco.CEP, '0'));                //327-334
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0335, 015, 0, boleto.Sacado.Endereco.Cidade.ToUpper(), ' '));   //335-349
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0350, 002, 0, boleto.Sacado.Endereco.UF.ToUpper(), ' '));       //350-351
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0352, 040, 0, string.Empty, ' '));                              //352-391
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0392, 002, 0, diasProtesto, ' '));                              //392-393
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0394, 001, 0, string.Empty, ' '));                              //394-394                
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediNumericoSemSeparador_, 0395, 006, 0, numeroRegistro, '0'));                            //395-400
                reg.CodificarLinha();

                string _detalhe = Utils.SubstituiCaracteresEspeciais(reg.LinhaRegistro);
                return _detalhe;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao gerar DETALHE do arquivo CNAB400.", ex);
            }
        }

        public string GerarTrailerRemessa400(int numeroRegistro, decimal vltitulostotal)
        {
            try
            {
                TRegistroEDI reg = new TRegistroEDI();
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0001, 001, 0, "9", ' '));            //001-001
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0002, 393, 0, string.Empty, ' '));   //002-393
                reg.CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediNumericoSemSeparador_, 0395, 006, 0, numeroRegistro, '0')); //395-400
                //
                reg.CodificarLinha();
                //
                string vLinha = reg.LinhaRegistro;
                string _trailer = Utils.SubstituiCaracteresEspeciais(vLinha);
                //
                return _trailer;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro durante a geração do registro TRAILER do arquivo de REMESSA.", ex);
            }
        }

        public override DetalheRetorno LerDetalheRetornoCNAB400(string registro)
        {
            try
            {
                TRegistroEDI_BancoBrasil_Retorno reg = new TRegistroEDI_BancoBrasil_Retorno();
                //
                reg.LinhaRegistro = registro;
                reg.DecodificarLinha();

                //Passa para o detalhe as propriedades de reg;
                DetalheRetorno detalhe = new DetalheRetorno(registro);
                //
                //detalhe. = reg.Identificacao;
                //detalhe. = reg.Zeros1;
                //detalhe. = reg.Zeros2;
                detalhe.Agencia = Utils.ToInt32(string.Concat(reg.PrefixoAgencia, reg.DVPrefixoAgencia));
                detalhe.Conta = Utils.ToInt32(reg.ContaCorrente);
                detalhe.DACConta = Utils.ToInt32(reg.DVContaCorrente);
                //detalhe. = reg.NumeroConvenioCobranca;
                //detalhe. = reg.NumeroControleParticipante;
                //
                detalhe.NossoNumeroComDV = reg.NossoNumero;
                detalhe.NossoNumero = reg.NossoNumero.Substring(0, reg.NossoNumero.Length - 1); //Nosso Número sem o DV!
                detalhe.DACNossoNumero = reg.NossoNumero.Substring(reg.NossoNumero.Length - 1); //DV
                //
                //detalhe. = reg.TipoCobranca;
                //detalhe. = reg.TipoCobrancaEspecifico;
                //detalhe. = reg.DiasCalculo;
                //detalhe. = reg.NaturezaRecebimento;
                //detalhe. = reg.PrefixoTitulo;
                //detalhe. = reg.VariacaoCarteira;
                //detalhe. = reg.ContaCaucao;
                //detalhe. = reg.TaxaDesconto;
                //detalhe. = reg.TaxaIOF;
                //detalhe. = reg.Brancos1;
                detalhe.Carteira = reg.Carteira;
                detalhe.CodigoOcorrencia = Utils.ToInt32(reg.Comando);
                //
                int dataLiquidacao = Utils.ToInt32(reg.DataLiquidacao);
                detalhe.DataLiquidacao = Utils.ToDateTime(dataLiquidacao.ToString("##-##-##"));
                //
                detalhe.NumeroDocumento = reg.NumeroTituloCedente;
                //detalhe. = reg.Brancos2;
                //
                int dataVencimento = Utils.ToInt32(reg.DataVencimento);
                detalhe.DataVencimento = Utils.ToDateTime(dataVencimento.ToString("##-##-##"));
                //
                detalhe.ValorTitulo = (Convert.ToDecimal(reg.ValorTitulo) / 100);
                detalhe.CodigoBanco = Utils.ToInt32(reg.CodigoBancoRecebedor);
                detalhe.AgenciaCobradora = Utils.ToInt32(reg.PrefixoAgenciaRecebedora);
                //detalhe. = reg.DVPrefixoRecebedora;
                detalhe.Especie = Utils.ToInt32(reg.EspecieTitulo);
                //
                int dataCredito = Utils.ToInt32(reg.DataCredito);
                detalhe.DataOcorrencia = Utils.ToDateTime(dataCredito.ToString("##-##-##"));
                //
                detalhe.TarifaCobranca = (Convert.ToDecimal(reg.ValorTarifa) / 100);
                detalhe.OutrasDespesas = (Convert.ToDecimal(reg.OutrasDespesas) / 100);
                detalhe.ValorOutrasDespesas = (Convert.ToDecimal(reg.JurosDesconto) / 100);
                detalhe.IOF = (Convert.ToInt64(reg.IOFDesconto) / 100);
                detalhe.Abatimentos = (Convert.ToDecimal(reg.ValorAbatimento) / 100);
                detalhe.Descontos = (Convert.ToDecimal(reg.DescontoConcedido) / 100);
                detalhe.ValorPrincipal = (Convert.ToDecimal(reg.ValorRecebido) / 100);
                detalhe.JurosMora = (Convert.ToDecimal(reg.JurosMora) / 100);
                detalhe.OutrosCreditos = (Convert.ToDecimal(reg.OutrosRecebimentos) / 100);
                //detalhe. = reg.AbatimentoNaoAproveitado;
                detalhe.ValorPago = (Convert.ToDecimal(reg.ValorLancamento) / 100);
                //detalhe. = reg.IndicativoDebitoCredito;
                //detalhe. = reg.IndicadorValor;
                //detalhe. = reg.ValorAjuste;
                //detalhe. = reg.Brancos3;
                //detalhe. = reg.Brancos4;
                //detalhe. = reg.Zeros3;
                //detalhe. = reg.Zeros4;
                //detalhe. = reg.Zeros5;
                //detalhe. = reg.Zeros6;
                //detalhe. = reg.Zeros7;
                //detalhe. = reg.Zeros8;
                //detalhe. = reg.Brancos5;
                //detalhe. = reg.CanalPagamento;
                //detalhe. = reg.NumeroSequenciaRegistro;
                #region NAO RETORNADOS PELO BANCO
                detalhe.MotivoCodigoOcorrencia = string.Empty;
                detalhe.MotivosRejeicao = string.Empty;
                detalhe.NumeroCartorio = 0;
                detalhe.NumeroProtocolo = string.Empty;
                detalhe.NomeSacado = string.Empty;
                #endregion

                return detalhe;
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao ler detalhe do arquivo de RETORNO / CNAB 400.", ex);
            }
        }

        #endregion

        public override long ObterNossoNumeroSemConvenioOuDigitoVerificador(long convenio, string nossoNumero)
        {
            if (string.IsNullOrEmpty(nossoNumero))
                throw new NossoNumeroInvalidoException();

            nossoNumero = nossoNumero.Trim();

            int tamanhoConvenio = convenio.ToString().Length;
            long result;
            switch (tamanhoConvenio)
            {
                case 4:
                    // Se convênio de 4 posições - normalmente carteira 17 - (0001 à 9999), informar NossoNumero com 11 caracteres, com DV, sendo:
                    // 4 posições do nº do convênio e 7 posições do nº de controle (nº do documento) e DV.
                    {
                        if (nossoNumero.Length != 12)
                            throw new TamanhoNossoNumeroInvalidoException();
                        var numeroSemConvenio = nossoNumero.Substring(4);
                        nossoNumero = numeroSemConvenio.Substring(0, 7);
                    }
                    break;
                case 6:
                    // Se convênio de 6 posições (acima de 10.000 à 999.999), informar NossoNumero com 11 caracteres + DV, sendo:
                    // 6 posições do nº do convênio e 5 posições do nº de controle (nº do documento) e DV do nosso numero.
                    {
                        if (nossoNumero.Length != 12)
                            throw new TamanhoNossoNumeroInvalidoException();
                        var numeroSemConvenio = nossoNumero.Substring(6);
                        nossoNumero = numeroSemConvenio.Substring(0, 5);
                    }
                    break;
                case 7:
                    // Se convênio de 7 posições (acima de 1.000.000 à 9.999.999), informar NossoNumero com 17 caracteres, sem DV, sendo:
                    // 7 posições do nº do convênio e 10 posições do nº de controle (nº do documento)
                    {
                        if (nossoNumero.Length != 17)
                            throw new TamanhoNossoNumeroInvalidoException();
                        nossoNumero = nossoNumero.Substring(7);
                    }
                    break;
                default:
                    throw new Exception("Posições do nº de convênio deve ser 4, 6 ou 7.");
            }
            if (long.TryParse(nossoNumero, out result))
                return result;
            throw new NossoNumeroInvalidoException();
        }
    }
}