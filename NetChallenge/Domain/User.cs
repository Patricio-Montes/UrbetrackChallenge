using NetChallenge.Domain.ValueObjects;
using System;

namespace NetChallenge.Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public PhoneNumber PhoneNumber { get; private set; }
        /// <summary>
        /// En un sistema mas sofisticado si el usuario proporcionara parte de su direccion, 
        /// a traves de la API de Google Maps se le pueden entregar oficinas disponibles en un radio de cercania.
        /// </summary>
        public Address Address { get; private set; }
    }
}
