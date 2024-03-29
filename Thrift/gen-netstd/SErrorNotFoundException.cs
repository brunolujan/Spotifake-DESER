/**
 * Autogenerated by Thrift Compiler (0.13.0)
 *
 * DO NOT EDIT UNLESS YOU ARE SURE THAT YOU KNOW WHAT YOU ARE DOING
 *  @generated
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Thrift;
using Thrift.Collections;

using Thrift.Protocol;
using Thrift.Protocol.Entities;
using Thrift.Protocol.Utilities;
using Thrift.Transport;
using Thrift.Transport.Client;
using Thrift.Transport.Server;
using Thrift.Processor;



/// <summary>
/// This exception is thrown by SError procedures when a caller asks to perform an operation on
/// an object that does not exist.
/// 
/// identifier: A description of the object that was not found on the serve.
/// 
/// key: The value passed from the client in the identifier, which was not found.
/// </summary>
public partial class SErrorNotFoundException : TException, TBase
{
  private string _key;

  public string Identifier { get; set; }

  public string Key
  {
    get
    {
      return _key;
    }
    set
    {
      __isset.key = true;
      this._key = value;
    }
  }


  public Isset __isset;
  public struct Isset
  {
    public bool key;
  }

  public SErrorNotFoundException()
  {
  }

  public SErrorNotFoundException(string identifier) : this()
  {
    this.Identifier = identifier;
  }

  public async Task ReadAsync(TProtocol iprot, CancellationToken cancellationToken)
  {
    iprot.IncrementRecursionDepth();
    try
    {
      bool isset_identifier = false;
      TField field;
      await iprot.ReadStructBeginAsync(cancellationToken);
      while (true)
      {
        field = await iprot.ReadFieldBeginAsync(cancellationToken);
        if (field.Type == TType.Stop)
        {
          break;
        }

        switch (field.ID)
        {
          case 1:
            if (field.Type == TType.String)
            {
              Identifier = await iprot.ReadStringAsync(cancellationToken);
              isset_identifier = true;
            }
            else
            {
              await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
            }
            break;
          case 2:
            if (field.Type == TType.String)
            {
              Key = await iprot.ReadStringAsync(cancellationToken);
            }
            else
            {
              await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
            }
            break;
          default: 
            await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
            break;
        }

        await iprot.ReadFieldEndAsync(cancellationToken);
      }

      await iprot.ReadStructEndAsync(cancellationToken);
      if (!isset_identifier)
      {
        throw new TProtocolException(TProtocolException.INVALID_DATA);
      }
    }
    finally
    {
      iprot.DecrementRecursionDepth();
    }
  }

  public async Task WriteAsync(TProtocol oprot, CancellationToken cancellationToken)
  {
    oprot.IncrementRecursionDepth();
    try
    {
      var struc = new TStruct("SErrorNotFoundException");
      await oprot.WriteStructBeginAsync(struc, cancellationToken);
      var field = new TField();
      field.Name = "identifier";
      field.Type = TType.String;
      field.ID = 1;
      await oprot.WriteFieldBeginAsync(field, cancellationToken);
      await oprot.WriteStringAsync(Identifier, cancellationToken);
      await oprot.WriteFieldEndAsync(cancellationToken);
      if (Key != null && __isset.key)
      {
        field.Name = "key";
        field.Type = TType.String;
        field.ID = 2;
        await oprot.WriteFieldBeginAsync(field, cancellationToken);
        await oprot.WriteStringAsync(Key, cancellationToken);
        await oprot.WriteFieldEndAsync(cancellationToken);
      }
      await oprot.WriteFieldStopAsync(cancellationToken);
      await oprot.WriteStructEndAsync(cancellationToken);
    }
    finally
    {
      oprot.DecrementRecursionDepth();
    }
  }

  public override bool Equals(object that)
  {
    var other = that as SErrorNotFoundException;
    if (other == null) return false;
    if (ReferenceEquals(this, other)) return true;
    return System.Object.Equals(Identifier, other.Identifier)
      && ((__isset.key == other.__isset.key) && ((!__isset.key) || (System.Object.Equals(Key, other.Key))));
  }

  public override int GetHashCode() {
    int hashcode = 157;
    unchecked {
      hashcode = (hashcode * 397) + Identifier.GetHashCode();
      if(__isset.key)
        hashcode = (hashcode * 397) + Key.GetHashCode();
    }
    return hashcode;
  }

  public override string ToString()
  {
    var sb = new StringBuilder("SErrorNotFoundException(");
    sb.Append(", Identifier: ");
    sb.Append(Identifier);
    if (Key != null && __isset.key)
    {
      sb.Append(", Key: ");
      sb.Append(Key);
    }
    sb.Append(")");
    return sb.ToString();
  }
}

