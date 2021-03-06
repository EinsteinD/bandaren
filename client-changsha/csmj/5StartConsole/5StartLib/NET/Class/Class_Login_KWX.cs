//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: protocols/Login.proto
namespace KWX
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"CMD_Login")]
  public partial class CMD_Login : global::ProtoBuf.IExtensible
  {
    public CMD_Login() {}
    
    private ulong _UserID = default(ulong);
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"UserID", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(ulong))]
    public ulong UserID
    {
      get { return _UserID; }
      set { _UserID = value; }
    }
    private string _Password = "";
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"Password", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string Password
    {
      get { return _Password; }
      set { _Password = value; }
    }
    private string _WeiXinCode = "";
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"WeiXinCode", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string WeiXinCode
    {
      get { return _WeiXinCode; }
      set { _WeiXinCode = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"RSP_Login")]
  public partial class RSP_Login : global::ProtoBuf.IExtensible
  {
    public RSP_Login() {}
    
    private ulong _UserID = default(ulong);
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"UserID", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(ulong))]
    public ulong UserID
    {
      get { return _UserID; }
      set { _UserID = value; }
    }
    private string _Password = "";
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"Password", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string Password
    {
      get { return _Password; }
      set { _Password = value; }
    }
    private uint _RoomID = default(uint);
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"RoomID", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(uint))]
    public uint RoomID
    {
      get { return _RoomID; }
      set { _RoomID = value; }
    }
    private string _GameName = "";
    [global::ProtoBuf.ProtoMember(4, IsRequired = false, Name=@"GameName", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string GameName
    {
      get { return _GameName; }
      set { _GameName = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"CMD_CreateRoom")]
  public partial class CMD_CreateRoom : global::ProtoBuf.IExtensible
  {
    public CMD_CreateRoom() {}
    
    private string _GameName = "";
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"GameName", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string GameName
    {
      get { return _GameName; }
      set { _GameName = value; }
    }
    private uint _RoundCount = default(uint);
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"RoundCount", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(uint))]
    public uint RoundCount
    {
      get { return _RoundCount; }
      set { _RoundCount = value; }
    }
    private string _Rule = "";
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"Rule", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string Rule
    {
      get { return _Rule; }
      set { _Rule = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"RSP_CreateRoom")]
  public partial class RSP_CreateRoom : global::ProtoBuf.IExtensible
  {
    public RSP_CreateRoom() {}
    
    private uint _RoomID = default(uint);
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"RoomID", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(uint))]
    public uint RoomID
    {
      get { return _RoomID; }
      set { _RoomID = value; }
    }
    private string _ServerHost = "";
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"ServerHost", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string ServerHost
    {
      get { return _ServerHost; }
      set { _ServerHost = value; }
    }
    private int _ServerPort = default(int);
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"ServerPort", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int ServerPort
    {
      get { return _ServerPort; }
      set { _ServerPort = value; }
    }
    private uint _SeatID = default(uint);
    [global::ProtoBuf.ProtoMember(4, IsRequired = false, Name=@"SeatID", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(uint))]
    public uint SeatID
    {
      get { return _SeatID; }
      set { _SeatID = value; }
    }
    private string _Password = "";
    [global::ProtoBuf.ProtoMember(5, IsRequired = false, Name=@"Password", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string Password
    {
      get { return _Password; }
      set { _Password = value; }
    }
    private string _ErrorInfo = "";
    [global::ProtoBuf.ProtoMember(6, IsRequired = false, Name=@"ErrorInfo", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string ErrorInfo
    {
      get { return _ErrorInfo; }
      set { _ErrorInfo = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"CMD_EnterRoom")]
  public partial class CMD_EnterRoom : global::ProtoBuf.IExtensible
  {
    public CMD_EnterRoom() {}
    
    private uint _RoomID = default(uint);
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"RoomID", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(uint))]
    public uint RoomID
    {
      get { return _RoomID; }
      set { _RoomID = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"RSP_EnterRoom")]
  public partial class RSP_EnterRoom : global::ProtoBuf.IExtensible
  {
    public RSP_EnterRoom() {}
    
    private int _Error = default(int);
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"Error", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int Error
    {
      get { return _Error; }
      set { _Error = value; }
    }
    private string _ServerHost = "";
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"ServerHost", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string ServerHost
    {
      get { return _ServerHost; }
      set { _ServerHost = value; }
    }
    private int _ServerPort = default(int);
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"ServerPort", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int ServerPort
    {
      get { return _ServerPort; }
      set { _ServerPort = value; }
    }
    private uint _SeatID = default(uint);
    [global::ProtoBuf.ProtoMember(4, IsRequired = false, Name=@"SeatID", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(uint))]
    public uint SeatID
    {
      get { return _SeatID; }
      set { _SeatID = value; }
    }
    private string _Password = "";
    [global::ProtoBuf.ProtoMember(5, IsRequired = false, Name=@"Password", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string Password
    {
      get { return _Password; }
      set { _Password = value; }
    }
    private uint _RoundCount = default(uint);
    [global::ProtoBuf.ProtoMember(6, IsRequired = false, Name=@"RoundCount", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(uint))]
    public uint RoundCount
    {
      get { return _RoundCount; }
      set { _RoundCount = value; }
    }
    private string _Rule = "";
    [global::ProtoBuf.ProtoMember(7, IsRequired = false, Name=@"Rule", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string Rule
    {
      get { return _Rule; }
      set { _Rule = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"CMD_EnterHall")]
  public partial class CMD_EnterHall : global::ProtoBuf.IExtensible
  {
    public CMD_EnterHall() {}
    
    private string _GameName = "";
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"GameName", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string GameName
    {
      get { return _GameName; }
      set { _GameName = value; }
    }
    private string _Rule = "";
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"Rule", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string Rule
    {
      get { return _Rule; }
      set { _Rule = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"RSP_EnterHall")]
  public partial class RSP_EnterHall : global::ProtoBuf.IExtensible
  {
    public RSP_EnterHall() {}
    
    private uint _RoomID = default(uint);
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"RoomID", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(uint))]
    public uint RoomID
    {
      get { return _RoomID; }
      set { _RoomID = value; }
    }
    private string _ServerHost = "";
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"ServerHost", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string ServerHost
    {
      get { return _ServerHost; }
      set { _ServerHost = value; }
    }
    private int _ServerPort = default(int);
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"ServerPort", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(int))]
    public int ServerPort
    {
      get { return _ServerPort; }
      set { _ServerPort = value; }
    }
    private uint _SeatID = default(uint);
    [global::ProtoBuf.ProtoMember(4, IsRequired = false, Name=@"SeatID", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(uint))]
    public uint SeatID
    {
      get { return _SeatID; }
      set { _SeatID = value; }
    }
    private string _Password = "";
    [global::ProtoBuf.ProtoMember(5, IsRequired = false, Name=@"Password", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string Password
    {
      get { return _Password; }
      set { _Password = value; }
    }
    private string _ErrorInfo = "";
    [global::ProtoBuf.ProtoMember(6, IsRequired = false, Name=@"ErrorInfo", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string ErrorInfo
    {
      get { return _ErrorInfo; }
      set { _ErrorInfo = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
}