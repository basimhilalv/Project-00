﻿namespace Project_00.Models
{
    public class PaymentCart
    {
        public int Id { get; set; }
        public DateTime PaymentDate { get; set; }
        public int Amount { get; set; }
        public Guid UserId { get; set; }
        public User? User { get; set; }
        public ICollection<Cart>? Products { get; set; }
    }
}
