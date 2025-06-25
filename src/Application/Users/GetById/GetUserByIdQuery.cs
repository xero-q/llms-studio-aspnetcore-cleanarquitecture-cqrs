﻿using Application.Abstractions.Messaging;

namespace Application.Users.GetById;

public sealed record GetUserByIdQuery(int UserId) : IQuery<UserResponse>;
