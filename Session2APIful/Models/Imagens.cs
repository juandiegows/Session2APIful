//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Session2APIful.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Imagens
    {
        public int Codigo { get; set; }
        public byte[] Arquivo { get; set; }
    
        public virtual Jogador Jogador { get; set; }
    }
}
