using System;

namespace ContractManager.Application.DTOs;

public class ClientResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}