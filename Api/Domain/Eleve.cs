using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Domain
{
    public class Eleve
    {
        public int IdEleve { get; set; }
        public int codeEleve { get; set; }
        public int id_StatutEleve { get; set; }
        public int id_Genre { get; set; }
        public int id_pays { get; set; }
        public int id_Provenance { get; set; }
        public int CD_ETAB { get; set; }
        public int cd_com_naissance { get; set; }
        public int IdTuteur { get; set; }
        public int id_handicap { get; set; }
        public int nomEleveFr { get; set; }
        public int nomEleveAr { get; set; }
        public int prenomEleveFr { get; set; }
        public int prenomEleveAr { get; set; }
        public int dateNaisEleve { get; set; }
        public int adresse { get; set; }
        public int nationalite { get; set; }
        public int DateModification { get; set; }
        public int UrlPhoto { get; set; }
        public int CINEleve { get; set; }
        public int CNE { get; set; }
        public int Lieu_naissance_Ar { get; set; }
        public int Lieu_naissance_Fr { get; set; }
        public int Actif { get; set; }
        public int id_eleve { get; set; }
    }
}
