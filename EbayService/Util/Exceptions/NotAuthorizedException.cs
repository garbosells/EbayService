﻿using System;
using System.Runtime.Serialization;

namespace EbayService.Controllers
{
  [Serializable]
  internal class NotAuthorizedException : Exception
  {
    public NotAuthorizedException()
    {
    }

    public NotAuthorizedException(string message) : base(message)
    {
    }

    public NotAuthorizedException(string message, Exception innerException) : base(message, innerException)
    {
    }

    protected NotAuthorizedException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
  }
}