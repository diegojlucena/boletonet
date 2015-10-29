
namespace BoletoNet.EDI.Banco
{
    /// <summary>
    /// Classe de Integração Banrisul
    /// CBR643
    /// Convênio 6 posições -- Único com Identificação do Registro Detalhe = 1
    /// </summary>
    public class RegistroEdiBancoBrasilRetornoConvenio6Digitos : TRegistroEDI_BancoBrasil_RetornoBase
    {

        public RegistroEdiBancoBrasilRetornoConvenio6Digitos()
        {
            /*
             * Aqui é que iremos informar as características de cada campo do arquivo
             * Na classe base, TCampoRegistroEDI, temos a propriedade CamposEDI, que é uma coleção de objetos
             * TCampoRegistroEDI.
             */

            #region TODOS os Campos

            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0001, 001, 0, string.Empty,
                ' ')); //001-001
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0002, 002, 0, string.Empty,
                ' ')); //002-003
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0004, 014, 0, string.Empty,
                ' ')); //004-017
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0018, 004, 0, string.Empty,
                ' ')); //018-021
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0022, 001, 0, string.Empty,
                ' ')); //022-022
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0023, 008, 0, string.Empty,
                ' ')); //023-030
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0031, 001, 0, string.Empty,
                ' ')); //031-031

            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0032, 006, 0, string.Empty,
                ' ')); //032-038

            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0038, 025, 0, string.Empty,
                ' ')); //039-063

            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0063, 012, 0, string.Empty,
                ' ')); //064-080

            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0075, 001, 0, string.Empty,
                ' ')); //081-081

            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0076, 001, 0, string.Empty,
                ' ')); //082-082

            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0077, 004, 0, string.Empty,
                ' ')); //083-086

            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0081, 002, 0, string.Empty,
                ' ')); //087-088

            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0083, 003, 0, string.Empty,
                ' ')); //089-091

            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0086, 003, 0, string.Empty,
                ' ')); //092-094

            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0089, 001, 0, string.Empty,
                ' ')); //095-095

            //this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0090, 005, 0, string.Empty, ' ')); //095-095 CodigoDeResponsabilidade
            //this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0095, 001, 0, string.Empty, ' ')); //095-095 CodigoDeResponsabilidadeDV


            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0096, 005, 0, string.Empty,
                ' ')); //096-100
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0101, 004, 0, string.Empty,
                ' ')); //101-105
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0106, 001, 0, string.Empty,
                ' ')); //106-106
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0107, 002, 0, string.Empty,
                ' ')); //107-108
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0109, 002, 0, string.Empty,
                ' ')); //109-110
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0111, 006, 0, string.Empty,
                ' ')); //111-116
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0117, 010, 0, string.Empty,
                ' ')); //117-126
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0127, 020, 0, string.Empty,
                ' ')); //127-146
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0147, 006, 0, string.Empty,
                ' ')); //147-152
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0153, 013, 0, string.Empty,
                ' ')); //153-165
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0166, 003, 0, string.Empty,
                ' ')); //166-168
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0169, 004, 0, string.Empty,
                ' ')); //169-172
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0173, 001, 0, string.Empty,
                ' ')); //173-173
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0174, 002, 0, string.Empty,
                ' ')); //174-175
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0176, 006, 0, string.Empty,
                ' ')); //176-181
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0182, 007, 0, string.Empty,
                ' ')); //182-188
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0189, 013, 0, string.Empty,
                ' ')); //189-201
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0202, 013, 0, string.Empty,
                ' ')); //202-214
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0215, 013, 0, string.Empty,
                ' ')); //215-227
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0228, 013, 0, string.Empty,
                ' ')); //228-240
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0241, 013, 0, string.Empty,
                ' ')); //241-253
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0254, 013, 0, string.Empty,
                ' ')); //254-266
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0267, 013, 0, string.Empty,
                ' ')); //267-279
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0280, 013, 0, string.Empty,
                ' ')); //280-292
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0293, 013, 0, string.Empty,
                ' ')); //293-305
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0306, 013, 0, string.Empty,
                ' ')); //306-318
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0319, 001, 0, string.Empty,
                ' ')); //319-319
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0320, 001, 0, string.Empty,
                ' ')); //320-320
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0321, 012, 0, string.Empty,
                ' ')); //321-332
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0333, 001, 0, string.Empty,
                ' ')); //333-333
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0334, 009, 0, string.Empty,
                ' ')); //334-342
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0343, 007, 0, string.Empty,
                ' ')); //343-349
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0350, 009, 0, string.Empty,
                ' ')); //350-358
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0359, 007, 0, string.Empty,
                ' ')); //359-365
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0366, 009, 0, string.Empty,
                ' ')); //366-374
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0375, 007, 0, string.Empty,
                ' ')); //375-381
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0382, 009, 0, string.Empty,
                ' ')); //382-390
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0391, 002, 0, string.Empty,
                ' ')); //391-392
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0393, 002, 0, string.Empty,
                ' ')); //393-394
            this._CamposEDI.Add(new TCampoRegistroEDI(TTiposDadoEDI.ediAlphaAliEsquerda_____, 0395, 006, 0, string.Empty,
                ' ')); //395-400

            #endregion
        }

    }

    /// <summary>
    /// Classe que irá representar o arquivo EDI em si
    /// </summary>
    public class TArquivoBancoBrasilRetornoConvenio6Digigos_EDI : TEDIFile
    {
        /*
         * De modo geral, apenas preciso sobreescrever o método de decodificação de linhas,
         * pois preciso adicionar um objeto do tipo registro na coleção do arquivo, passar a linha que vem do arquivo
         * neste objeto novo, e decodificá-lo para separar nos campos.
         * O DecodeLine é chamado a partir do método LoadFromFile() (ou Stream) da classe base.
         */
        protected override void DecodeLine(string Line)
        {
            base.DecodeLine(Line);
            Lines.Add(new RegistroEdiBancoBrasilRetornoConvenio6Digitos()); //Adiciono a linha a ser decodificada
            Lines[Lines.Count - 1].LinhaRegistro = Line; //Atribuo a linha que vem do arquivo
            Lines[Lines.Count - 1].DecodificarLinha(); //Finalmente, a separação das substrings na linha do arquivo.
        }
    }

}
