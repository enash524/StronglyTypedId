namespace StronglyTypedId.Tests.Types
{
    [StronglyTypedId(backingType: StronglyTypedIdBackingType.Double)]
    partial struct DoubleId { }

    [StronglyTypedId(generateJsonConverter: false, backingType: StronglyTypedIdBackingType.Double)]
    public partial struct NoJsonDoubleId { }

    [StronglyTypedId(jsonConverter: StronglyTypedIdJsonConverter.NewtonsoftJson, backingType: StronglyTypedIdBackingType.Double)]
    public partial struct NewtonsoftJsonDoubleId { }

    [StronglyTypedId(jsonConverter: StronglyTypedIdJsonConverter.SystemTextJson, backingType: StronglyTypedIdBackingType.Double)]
    public partial struct SystemTextJsonDoubleId { }

    [StronglyTypedId(jsonConverter: StronglyTypedIdJsonConverter.NewtonsoftJson | StronglyTypedIdJsonConverter.SystemTextJson, backingType: StronglyTypedIdBackingType.Double)]
    public partial struct BothJsonDoubleId { }
}