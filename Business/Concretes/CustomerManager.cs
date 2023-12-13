using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Business.Abstracts;
using Business.Dtos.Requests;
using Business.Dtos.Responses;
using Business.Rules;
using Core.DataAccess.Paging;
using DataAccess.Abstract;
using DataAccess.Concretes;
using Entities.Concretes;

namespace Business.Concretes
{
    public class CustomerManager : ICustomerService
    {
        ICustomerDal _customerDal;
        IMapper _mapper;
        CustomerBusinessRules _customerBusinessRules;


        public CustomerManager(ICustomerDal customerDal, IMapper mapper, CustomerBusinessRules customerBusinessRules)
        {
            _customerDal = customerDal;
            _mapper = mapper;
            _customerBusinessRules = customerBusinessRules;
        }

        public async Task<CreatedCustomerResponse> Add(CreateCustomerRequest createCustomerRequest)
        {
            await _customerBusinessRules.MaxCustomerTenPerCity(createCustomerRequest.City);
            await _customerBusinessRules.ContactNameCantRepeat(createCustomerRequest.ContactName);

            Customer mappedRequest = _mapper.Map<Customer>(createCustomerRequest);

            Customer createdCustomer = await _customerDal.AddAsync(mappedRequest);

            CreatedCustomerResponse createdCustomerResponse = _mapper.Map<CreatedCustomerResponse>(createdCustomer);

            return createdCustomerResponse;

        }

        public async Task<DeletedCustomerResponse> Delete(DeleteCustomerRequest deleteCustomerRequest)
        {
            Customer customer = _mapper.Map<Customer>(deleteCustomerRequest);

            var deletedCustomer = await _customerDal.DeleteAsync(customer);

            DeletedCustomerResponse deletedCustomerResponse = _mapper.Map<DeletedCustomerResponse>(deletedCustomer);

            return deletedCustomerResponse;
        }

        public async Task<CreatedCustomerResponse> GetById(string id)
        {
            var result = await _customerDal.GetAsync(c => c.Id == id);
            Customer mappedCustomer = _mapper.Map<Customer>(result);

            CreatedCustomerResponse createdCustomerResponse = _mapper.Map<CreatedCustomerResponse>(mappedCustomer);

            return createdCustomerResponse;
        }

        public async Task<IPaginate<GetListCustomerResponse>> GetListAsync(PageRequest pageRequest)
        {
            var data = await _customerDal.GetListAsync();

            var result = _mapper.Map<Paginate<GetListCustomerResponse>>(data);

            return result;
        }

        public async Task<UpdatedCustomerResponse> Update(UpdateCustomerRequest updateCustomerRequest)
        {
            Customer customer = _mapper.Map<Customer>(updateCustomerRequest);

            var updatedCustomer = await _customerDal.UpdateAsync(customer);

            UpdatedCustomerResponse updatedCustomerResponse = _mapper.Map<UpdatedCustomerResponse>(updatedCustomer);

            return updatedCustomerResponse;
        }
    }
}
