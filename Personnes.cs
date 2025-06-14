using System;

namespace Crud_Personne
{
    public class Personne
    {
        // Propriétés
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenoms { get; set; }
        public int Age { get; set; }
        public string NumeroTel { get; set; }

        // Constructeur vide
        public Personne() { }

        // Constructeur sans ID (utilisé pour ajout)
        public Personne(string nom, string prenoms, int age, string numeroTel)
        {
            Nom = nom;
            Prenoms = prenoms;
            Age = age;
            NumeroTel = numeroTel;
        }

        // Constructeur avec ID (utilisé pour modification)
        public Personne(int id, string nom, string prenoms, int age, string numeroTel)
        {
            Id = id;
            Nom = nom;
            Prenoms = prenoms;
            Age = age;
            NumeroTel = numeroTel;
        }
    }
}
