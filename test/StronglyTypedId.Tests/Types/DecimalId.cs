namespace StronglyTypedId.Tests.Types
{
    [StronglyTypedId(backingType: StronglyTypedIdBackingType.Decimal)]
    partial struct DecimalId { }

    [StronglyTypedId(generateJsonConverter: false, backingType: StronglyTypedIdBackingType.Decimal)]
    public partial struct NoJsonDecimalId { }

    [StronglyTypedId(jsonConverter: StronglyTypedIdJsonConverter.NewtonsoftJson, backingType: StronglyTypedIdBackingType.Decimal)]
    public partial struct NewtonsoftJsonDecimalId { }

    [StronglyTypedId(jsonConverter: StronglyTypedIdJsonConverter.SystemTextJson, backingType: StronglyTypedIdBackingType.Decimal)]
    public partial struct SystemTextJsonDecimalId { }

    [StronglyTypedId(jsonConverter: StronglyTypedIdJsonConverter.NewtonsoftJson | StronglyTypedIdJsonConverter.SystemTextJson, backingType: StronglyTypedIdBackingType.Decimal)]
    public partial struct BothJsonDecimalId { }
}
