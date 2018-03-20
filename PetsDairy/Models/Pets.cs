using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PetsDairy.Models
{
    public class Pet : TableEntity
    {
        public Pet()
        {
        }

        public Pet(string owner) : this()
        {
            PartitionKey = owner;
        }

        public string NickName { get; set; }

        public string Gender { get; set; }

        public DateTime Birthday { get; set; }
    }
}