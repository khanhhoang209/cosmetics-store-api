﻿using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CosmeticsStore.API.Configs;

public class KebabCaseModelBinderProvider : IModelBinderProvider
{
    public IModelBinder? GetBinder(ModelBinderProviderContext context)
    {
        // Apply the binder only for complex types, such as DTOs
        if (context.BindingInfo.BindingSource != BindingSource.Query)
            return null;

        return new KebabCaseModelBinder();
    }
}