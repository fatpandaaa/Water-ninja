#import "RevMobUnityiOSDelegate.h"
#import "RevMobAds.h"
#import "RevMobAdLink.h"

void UnitySendMessage(const char *className, const char *methodName, const char *param);

#define TO_NSSTRING( _x_ ) ( _x_ != NULL ) ? [NSString stringWithUTF8String:_x_] : [NSString stringWithUTF8String:""]
#define CREATE_REVMOB_DELEGATE(_AD_TYPE_) [[RevMobUnityiOSDelegate alloc] initWithAdType:_AD_TYPE_]

// Converts NSString to C style string by way of copy (Mono will free it)
#define STRCOPY( _x_ ) ( _x_ != NULL && [_x_ isKindOfClass:[NSString class]] ) ? strdup( [_x_ UTF8String] ) : NULL

static NSDictionary* revmobDelegatesDict = nil;
static NSDictionary* revmobDelegates() {
    if (revmobDelegatesDict == nil) {
        revmobDelegatesDict = [[NSDictionary alloc] initWithObjectsAndKeys:
                              CREATE_REVMOB_DELEGATE(@"Fullscreen"), @"Fullscreen",
                              CREATE_REVMOB_DELEGATE(@"Banner"), @"Banner",
                              CREATE_REVMOB_DELEGATE(@"Popup"), @"Popup",
                              CREATE_REVMOB_DELEGATE(@"Link"), @"Link", 
                              CREATE_REVMOB_DELEGATE(@"Session"), @"Session", nil];
    }
    return revmobDelegatesDict;
}

void RevMobUnityiOSBinding_setGameObjectDelegateCallback(const char* gameObjectName) {
    for (RevMobUnityiOSDelegate* revmobDelegate in [revmobDelegates() allValues]) {
        NSLog(@"Set delegate callback to %@", TO_NSSTRING(gameObjectName));
        revmobDelegate.gameObjectName = TO_NSSTRING(gameObjectName);
    }
}

@interface RevMobAds()
+ (void) startSessionWithAppID:(NSString *)appID delegate:(NSObject<RevMobAdsDelegate> *)delegate
                   testingMode:(RevMobAdsTestingMode)testingMode
                       sdkName:(NSString *)sdkName
                    sdkVersion:(NSString *)sdkVersion;
+ (RevMobAds *)sharedObject;
@end

static RevMobAds *revmob = nil;

void RevMobUnityiOSBinding_startSession(const char* appId, const char* version) {
    [RevMobAds startSessionWithAppID:TO_NSSTRING(appId)
                  withSuccessHandler:^{
                      revmob = [RevMobAds session];
                      NSLog(@"Session started with block");
                  } andFailHandler:^(NSError *error) {
                      NSLog(@"Session failed to start with block");
                  }];
}

void RevMobUnityiOSBinding_setTestingMode(int testingMode) {
    [RevMobAds session].testingMode = testingMode;
}

void RevMobUnityiOSBinding_setTimeoutInSeconds(int timeout) {
    [RevMobAds session].connectionTimeout = timeout;
}

NSMutableArray* supportedInterfaceOrientations(int *orientations) {
    NSMutableArray *arrayOfOrientations = nil;
    if (orientations && orientations[0] >= 1 && orientations[0] <= 4) {
        arrayOfOrientations = [[NSMutableArray alloc] initWithCapacity:4];
        for (int i = 0; i < 4; i++) {
            [arrayOfOrientations addObject:[NSNumber numberWithInt:orientations[i]]];
            if (orientations[i] < 1 || orientations[i] > 4) {
                break;
            }
        }
    }
    return arrayOfOrientations;
}

int* supportedOrientations() {
    static int orientations[4];
    
    typedef enum interfaceOrientation {
        UIInterfaceOrientationPortrait              = 1,
        UIInterfaceOrientationPortraitUpsideDown    = 2,
        UIInterfaceOrientationLandscapeRight        = 3,
        UIInterfaceOrientationLandscapeLeft         = 4
    } InterfaceOrientation;
    
    NSDictionary *interfaces = [NSDictionary dictionaryWithObjectsAndKeys:
                                [NSNumber numberWithInteger:UIInterfaceOrientationPortrait],            @"UIInterfaceOrientationPortrait",
                                [NSNumber numberWithInteger:UIInterfaceOrientationPortraitUpsideDown],  @"UIInterfaceOrientationPortraitUpsideDown",
                                [NSNumber numberWithInteger:UIInterfaceOrientationLandscapeRight],      @"UIInterfaceOrientationLandscapeRight",
                                [NSNumber numberWithInteger:UIInterfaceOrientationLandscapeLeft],       @"UIInterfaceOrientationLandscapeLeft",
                                nil];

    NSArray *arrayOfOrientations = [[[NSBundle mainBundle] infoDictionary] objectForKey:@"UISupportedInterfaceOrientations"];
    
    for (int i = 0; i < [arrayOfOrientations count]; i++) {
        NSString *interface = [arrayOfOrientations objectAtIndex:i];
        int interfaceValue = [[interfaces objectForKey:interface] integerValue];
        orientations[i] = interfaceValue;
    }
    return orientations;
}

#pragma mark Fullscreen

static RevMobFullscreen *fullscreen = nil;

void RevMobUnityiOSBinding_createFullscreen(const char* placementId) {
    if (fullscreen != nil) {
        [fullscreen release];
        fullscreen = nil;
    }
    if (placementId == NULL) {
        fullscreen = [[revmob fullscreen] retain];
    } else {
        NSLog(@"Using placement id: %@", TO_NSSTRING(placementId));
        fullscreen = [[revmob fullscreenWithPlacementId:TO_NSSTRING(placementId)] retain];
    }
    fullscreen.delegate = [revmobDelegates() valueForKey:@"Fullscreen"];    
    fullscreen.supportedInterfaceOrientations = supportedInterfaceOrientations(supportedOrientations());

}

void RevMobUnityiOSBinding_loadFullscreen(const char* placementId) {
    RevMobUnityiOSBinding_createFullscreen(placementId);
    [fullscreen loadAd];
}

void RevMobUnityiOSBinding_showFullscreen(const char* placementId) {
    RevMobUnityiOSBinding_createFullscreen(placementId);
    [fullscreen showAd];
}

void RevMobUnityiOSBinding_hideLoadedFullscreen() {
    if (fullscreen != nil) [fullscreen hideAd];
}

void RevMobUnityiOSBinding_showLoadedFullscreen() {
    if (fullscreen != nil) [fullscreen showAd];
}

void RevMobUnityiOSBinding_releaseLoadedFullscreen() {
    NSLog(@"Releasing fullscreen.");
    [fullscreen release];
    fullscreen = nil;
}

void RevMobUnityiOSBinding_loadFullscreenWithSpecificOrientations(int *orientations) {
    RevMobUnityiOSBinding_loadFullscreen(NULL);
    fullscreen.supportedInterfaceOrientations = supportedInterfaceOrientations(orientations);
}

void RevMobUnityiOSBinding_showFullscreenWithSpecificOrientations(int *orientations) {
    RevMobUnityiOSBinding_loadFullscreenWithSpecificOrientations(orientations);
    [fullscreen showAd];
}

#pragma mark Banner

static RevMobBanner *revmobBanner = nil;

void RevMobUnityiOSBinding_deactivateBannerAd() {
    if (revmobBanner != nil) {
        [revmobBanner release];
        revmobBanner = nil;
    }
}

void RevMobUnityiOSBinding_loadBanner(const char *placementId, float x, float y, float width, float height) {
    RevMobUnityiOSBinding_deactivateBannerAd();
    if (placementId == NULL) {
        revmobBanner = [[revmob banner] retain];
    } else {
        NSLog(@"Using placement id: %@", TO_NSSTRING(placementId));
        revmobBanner = [[revmob bannerWithPlacementId:TO_NSSTRING(placementId)] retain];
    }
    revmobBanner.delegate = [revmobDelegates() valueForKey:@"Banner"];
    revmobBanner.supportedInterfaceOrientations = supportedInterfaceOrientations(supportedOrientations());
    [revmobBanner setFrame:CGRectMake(x, y, width, height)];
}

void RevMobUnityiOSBinding_showBanner(const char *placementId, int *orientations, float x, float y, float width, float height) {
    RevMobUnityiOSBinding_loadBanner(placementId, x, y, width, height);
    revmobBanner.supportedInterfaceOrientations = supportedInterfaceOrientations(supportedOrientations());
    [revmobBanner showAd];
}

void RevMobUnityiOSBinding_hideBanner() {
    if (revmobBanner != nil) [revmobBanner hideAd];
}

#pragma mark Ad Link

static RevMobAdLink *revmobAdLink = nil;

void RevMobUnityiOSBinding_loadAdLink(const char *placementId) {
    if ( revmobAdLink != nil) {
        [revmobAdLink release];
        revmobAdLink = nil;
    }
    revmobAdLink = (placementId == NULL) ? [revmob adLink] : [revmob adLinkWithPlacementId:TO_NSSTRING(placementId)];
    [revmobAdLink retain];
    revmobAdLink.delegate = [revmobDelegates() valueForKey:@"Link"];
    [revmobAdLink loadAd];
}

void RevMobUnityiOSBinding_openAdLink(const char *placementId) {
    RevMobUnityiOSBinding_loadAdLink(placementId);
    [revmobAdLink openLink];
}

void RevMobUnityiOSBinding_openLoadedAdLink() {
    [revmobAdLink openLink];
}

#pragma mark Popup

void RevMobUnityiOSBinding_showPopup(const char *placementId) {
    if (placementId == NULL) {
        RevMobPopup *popup = [revmob popup];
        popup.delegate = [revmobDelegates() valueForKey:@"Popup"];
        [popup showAd];
    } else {
        if (revmob != nil) {
            NSLog(@"Using placement id: %@", TO_NSSTRING(placementId));
            RevMobPopup *popup = [revmob popupWithPlacementId: TO_NSSTRING(placementId)];
            popup.delegate = [revmobDelegates() valueForKey:@"Popup"];
            [popup showAd];
        }
    }
}


void RevMobUnityiOSBinding_printEnvironmentInformation() {
    if (revmob) {
        [revmob printEnvironmentInformation];
    }
}


@implementation RevMobUnityiOSDelegate

- (RevMobUnityiOSDelegate *)initWithAdType:(NSString *)theAdType {
    self = [super init];
    self.gameObjectName = @"RevMobEventListener";
    adType = theAdType;
    return self;
}

- (void)revmobSessionIsStarted {
	UnitySendMessage(STRCOPY(self.gameObjectName), "SessionIsStarted", STRCOPY(adType));
}

- (void)revmobSessionNotStartedWithError:(NSError *)error {
	UnitySendMessage(STRCOPY(self.gameObjectName), "SessionNotStarted", STRCOPY(adType));
    NSLog(@"%@", error);
}

- (void)revmobAdDidReceive {
	UnitySendMessage(STRCOPY(self.gameObjectName), "AdDidReceive", STRCOPY(adType));
}

- (void)revmobAdDidFailWithError:(NSError *)error {
	UnitySendMessage(STRCOPY(self.gameObjectName), "AdDidFail", STRCOPY(adType));
    if ([adType isEqualToString:@"Fullscreen"]) {
        RevMobUnityiOSBinding_releaseLoadedFullscreen();
    }
    NSLog(@"%@", error);
}

- (void)revmobAdDisplayed {
	UnitySendMessage(STRCOPY(self.gameObjectName), "AdDisplayed", STRCOPY(adType));
}

- (void)revmobUserClickedInTheAd {
	UnitySendMessage(STRCOPY(self.gameObjectName), "UserClickedInTheAd", STRCOPY(adType));
    if ([adType isEqualToString:@"Fullscreen"]) {
        RevMobUnityiOSBinding_releaseLoadedFullscreen();
    }

}

- (void)revmobUserClosedTheAd {
	UnitySendMessage(STRCOPY(self.gameObjectName), "UserClosedTheAd", STRCOPY(adType));
    if ([adType isEqualToString:@"Fullscreen"]) {
        RevMobUnityiOSBinding_releaseLoadedFullscreen();
    }
}

# pragma mark Advertiser Callbacks

- (void)installDidReceive {
	UnitySendMessage(STRCOPY(self.gameObjectName), "InstallDidReceive", "InstallDidReceive");
}

- (void)installDidFail {
	UnitySendMessage(STRCOPY(self.gameObjectName), "InstallDidFail", "InstallDidFail");
}
@end