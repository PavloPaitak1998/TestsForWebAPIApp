using AutoMapper;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;
using Shared.DTO;
using Shared.Exceptions;
using System.Collections.Generic;

namespace BusinessLogicLayer.Services
{
    public class TicketService : ITicketService
    {
        IRepository<Ticket> ticketRepository;
        IMapper mapper = new MapperConfiguration(cfg => cfg.CreateMap<TicketDTO, Ticket>()).CreateMapper();

        public TicketService(IRepository<Ticket> _ticketRepository)
        {
            ticketRepository = _ticketRepository;
        }

        public TicketDTO GetEntity(int id)
        {
            var ticket = ticketRepository.Get(id);

            if (ticket == null)
                throw new ValidationException($"Ticket with this id {id} not found");

            return mapper.Map<TicketDTO>(ticket);
        }

        public IEnumerable<TicketDTO> GetEntities()
        {
            return mapper.Map<IEnumerable<Ticket>, List<TicketDTO>>(ticketRepository.GetAll());
        }

        public void CreateEntity(TicketDTO ticketDTO)
        {
            ticketRepository.Add(mapper.Map<Ticket>(ticketDTO));
        }

        public void UpdateEntity(int id, TicketDTO ticketDTO)
        {
            var ticket = ticketRepository.Get(id);

            if (ticket == null)
                throw new ValidationException($"Ticket with this id {ticketDTO.Id} not found");
            if (ticketDTO.Price > 0)
                ticket.Price = ticketDTO.Price;
            if (ticketDTO.FlightNumber > 0)
                ticket.FlightNumber = ticketDTO.FlightNumber;
            if (ticketDTO.FlightId > 0)
                ticket.FlightId = ticketDTO.FlightId;


            ticketRepository.Update(ticket);
        }

        public void DeleteAllEntities()
        {
            ticketRepository.DeleteAll();
        }

        public void DeleteEntity(int id)
        {
            var ticket = ticketRepository.Get(id);

            if (ticket == null)
                throw new ValidationException($"Ticket with this id {id} not found");

            ticketRepository.Delete(ticket);
        }
    }
}
