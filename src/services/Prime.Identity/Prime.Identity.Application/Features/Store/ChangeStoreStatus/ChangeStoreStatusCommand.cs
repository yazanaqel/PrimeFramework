using Application.Abstractions.Messaging;
using Prime.Identity.Application.Features.Store.Create;
using System;
using System.Collections.Generic;
using System.Text;

namespace Prime.Identity.Application.Features.Store.ChangeStoreStatus;

public sealed record ChangeStoreStatusCommand(ChangeStoreStatusRequest Request,CancellationToken ct) : ICommand<bool>;