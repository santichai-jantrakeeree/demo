using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Dtos;
using Core.Entities;
using Core.Entities.Orders;
using Core.Interfaces;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;

namespace Application.Services
{
    public class OrderService : DbService<int, Order, OrderDto, IOrderRepository>, IOrderService
    {
        private readonly IMapper _mapper;
        private IBasketService _basketService;
        private IProductRepository _productRepository;
        private IDeliveryMethodRepository _deliveryMethodRepository;

        public OrderService(IMapper mapper, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _mapper = mapper;
        }

        #region Properties
        protected IBasketService BasketService
        {
            get
            {
                if (_basketService == null)
                {
                    _basketService = _unitOfWork.GetService(typeof(IBasketService)) as IBasketService;
                }
                return _basketService;
            }
        }
        //we use repository because we don't need a product dto in use.
        protected IProductRepository ProductRepository
        {
            get
            {
                if (_productRepository == null)
                {
                    _productRepository = _unitOfWork.GetRepository(typeof(IProductRepository)) as IProductRepository;
                }
                return _productRepository;
            }
        }

        protected IDeliveryMethodRepository DeliveryMethodRepository
        {
            get
            {
                if (_deliveryMethodRepository == null)
                {
                    _deliveryMethodRepository = _unitOfWork.GetRepository(typeof(IDeliveryMethodRepository)) as IDeliveryMethodRepository;
                }
                return _deliveryMethodRepository;
            }
        }
        #endregion Properties

        public async Task<OrderDto> CreateOrder(OrderDto dto)
        {
            dto.UserId = _unitOfWork.CurrentUserId.Value;
            dto.BuyerEmail = _unitOfWork.CurrentUserEmail;
            dto.OrderDate = DateTime.Now;
            dto.SubTotal = dto.OrderItems.Sum(o => o.Price * o.Quantity);
            var deliveryMethod = await DeliveryMethodRepository.FindAsync(dto.DeliveryMethodId);
            var deliveryInfo = _mapper.Map<DeliveryMethod, DeliveryInfo>(deliveryMethod);
            var orderItems = _mapper.Map<IList<OrderItemDto>, IList<OrderItem>>(dto.OrderItems);
            Order order = new Order(orderItems, dto.BuyerEmail, dto.ShipToAddress, deliveryInfo, dto.SubTotal);
            this.Add(order);
            return dto;
        }
    }
}