#import "UnityAppController.h"
#import "WXApi.h"
#import "NSDictionary+BVJSONString.h"

@interface MahjongAppController : UnityAppController <WXApiDelegate>

@end

IMPL_APP_CONTROLLER_SUBCLASS (MahjongAppController)

@implementation MahjongAppController

- (BOOL)application:(UIApplication*)application didFinishLaunchingWithOptions:(NSDictionary*)launchOptions
{
    BOOL ret = [super application:application didFinishLaunchingWithOptions:launchOptions];
    if (ret)
        [WXApi registerApp: @"wx25be6ea93b70ff37"];
    return ret;
}

- (BOOL)application:(UIApplication*)application openURL:(NSURL*)url sourceApplication:(NSString*)sourceApplication annotation:(id)annotation {
    return [WXApi handleOpenURL:url delegate:self];
}

- (BOOL)application:(UIApplication *)application handleOpenURL:(NSURL *)url {
    return [WXApi handleOpenURL:url delegate:self];
}

- (void)onResp:(BaseResp *)resp {
    // 登录授权
    if([resp isKindOfClass:[SendAuthResp class]]) {
        SendAuthResp *response = (SendAuthResp *)resp;
        NSDictionary *dict = @{ @"errCode"  : @(response.errCode),
                                @"errStr"   : response.errStr ?: [NSNull null],
                                @"type"     : @(response.type),
                                @"code"     : response.code ?: [NSNull null],
                                @"state"    : response.state ?: [NSNull null],
                                @"lang"     : response.lang ?: [NSNull null],
                                @"country"  : response.country ?: [NSNull null],
                                };
        UnitySendMessage("MJApp", "OnWeChatSuccessfulLogin", [[dict bv_jsonStringWithPrettyPrint:NO] cStringUsingEncoding:NSUTF8StringEncoding]);
    }
    // 分享
    else if ([resp isKindOfClass:[SendMessageToWXResp class]]) {
        SendMessageToWXResp *response = (SendMessageToWXResp *)resp;
        NSDictionary *dict = @{ @"errCode"  : @(response.errCode),
                                @"errStr"   : response.errStr ?: [NSNull null],
                                @"type"     : @(response.type),
                                @"lang"     : response.lang ?: [NSNull null],
                                @"country"  : response.country ?: [NSNull null],
                                };
        UnitySendMessage("MJApp", "OnWeChatSuccessfulShared", [[dict bv_jsonStringWithPrettyPrint:NO] cStringUsingEncoding:NSUTF8StringEncoding]);
    }
}

- (void)onReq:(BaseReq *)req {
    // do something
}

@end


extern "C"
{
    void _WXLogin() {
        if ([WXApi isWXAppInstalled]) {
            SendAuthReq *req = [[SendAuthReq alloc] init];
            req.scope = @"snsapi_userinfo";
            req.state = @"App";
            [WXApi sendReq:req];
        } else {
            UIAlertController *alert = [UIAlertController alertControllerWithTitle:@"温馨提示" message:@"请先安装微信客户端" preferredStyle:UIAlertControllerStyleAlert];
            UIAlertAction *actionConfirm = [UIAlertAction actionWithTitle:@"确定" style:UIAlertActionStyleDefault handler:nil];
            [alert addAction:actionConfirm];
            [[UIApplication sharedApplication].keyWindow.rootViewController presentViewController:alert animated:YES completion:nil];
        }
    }

    void _WXShareTextToFriends(int scene, const char *text)
    {
        SendMessageToWXReq* req = [[SendMessageToWXReq alloc] init];
        req.bText = YES;
        req.scene = scene;
        req.text = [NSString stringWithUTF8String:text];
        [WXApi sendReq:req];
    }

    void _WXShareWebpageMessageToFriends(int scene, const char *title, const char *desc, const char *url)
    {
        SendMessageToWXReq* req = [[SendMessageToWXReq alloc] init];
        req.bText = NO;
        req.message = ^(void){
            WXMediaMessage *message = [WXMediaMessage message];
            message.title = [NSString stringWithUTF8String:title];
            message.description = [NSString stringWithUTF8String:desc];
            message.mediaTagName = @"ShareWebpageMessageToFriends";
            message.mediaObject = ^(void){
                WXWebpageObject *object = [WXWebpageObject object];
                object.webpageUrl = [NSString stringWithUTF8String:url];
                return object;
            }();
            [message setThumbImage:[UIImage imageNamed:@"AppIcon"]];
            return message;
        }();
        req.scene = scene;
        [WXApi sendReq:req];
    }

    void _WXShareImageMessageToFriends(int scene, const char *title, const char *desc, const char *url)
    {
        SendMessageToWXReq* req = [[SendMessageToWXReq alloc] init];
        req.bText = NO;
        req.message = ^(void){
            WXMediaMessage *message = [WXMediaMessage message];
            message.title = [NSString stringWithUTF8String:title];
            message.description = [NSString stringWithUTF8String:desc];
            message.mediaTagName = @"ShareImageMessageToFriends";
            message.mediaObject = ^(void){
                WXImageObject *object = [WXImageObject object];
                object.imageData = [NSData dataWithContentsOfFile:[NSString stringWithUTF8String:url] ?: @""];
                return object;
            }();
            [message setThumbImage:[UIImage imageNamed:@"AppIcon"]];
            return message;
        }();
        req.scene = scene;
        [WXApi sendReq:req];
    }
}
