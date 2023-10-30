// Ignore Spelling: Dto

using AutoMapper;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using FiberFresh.Application.Dtos;
using FiberFresh.Domain.Entities;
using FiberFresh.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace FiberFresh.Application.Services
{
    public class FiberFreshService : IFiberFreshService
    {
        private readonly IFiberFreshRepository _repository;
        private readonly IMapper _mapper;

        public FiberFreshService(IFiberFreshRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<HttpStatusCode> Create(BookingDto bookingDto)
        {
            if (bookingDto.Services.SelectMany(x => x.Images).Any(x => x != null))
            {
                var connectionString = Environment.GetEnvironmentVariable("FIBERFRESHBLOB");

                var blobServiceClient = new BlobServiceClient(connectionString);

                var containerName = "fiberfresh";

                var blobContainerClient = blobServiceClient.GetBlobContainerClient(containerName);

                await blobContainerClient.CreateIfNotExistsAsync();


                var services = _mapper.Map<List<Service>>(bookingDto.Services);

                var booking = _mapper.Map<Booking>(bookingDto);
                booking.Services = services;

                services = await _repository.Create(booking);

                foreach (var service in bookingDto.Services)
                {
                    foreach (var image in service.Images)
                    {
                        var blobClient = blobContainerClient.GetBlobClient(DateTime.UtcNow.Ticks.ToString());

                        var blobHttpHeaders = new BlobHttpHeaders
                        {
                            ContentType = image.Blob.ContentType
                        };

                        var stream = image.Blob.OpenReadStream();
                        var response = await blobClient.UploadAsync(stream, blobHttpHeaders);

                        var metaData = new Dictionary<string, string>
                            {
                                { "serviceid", services[bookingDto.Services.IndexOf(service)].Id.ToString() }
                            };

                        await blobClient.SetMetadataAsync(metaData);

                        var tags = new Dictionary<string, string>
                            {
                                { "furniture", service.Furniture.ToString() },
                                { "fabric", service.Fabric.ToString() }
                            };

                        await blobClient.SetTagsAsync(tags);

                        stream.Close();

                        if (response.GetRawResponse().Status != StatusCodes.Status201Created)
                        {
                            return HttpStatusCode.BadRequest;
                        }
                    }
                }
                return HttpStatusCode.OK;
            }
            else
            {
                return HttpStatusCode.BadRequest;
            }
        }

        public async Task<List<DateOfService>> GetDates() =>
            await _repository.GetDates();

        public async Task AddDate(DateOfService dateOfService) => 
            await _repository.AddDate(dateOfService);

        public Task AddDates(List<DateOfService> dateOfServicePool)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteDate(DateOfService dateOfService) =>
            await _repository.DeleteDate(dateOfService);
    }
}