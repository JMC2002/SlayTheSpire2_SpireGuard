<p align="center">
  <a href="README.md"><img alt="中文" src=".github/badges/language-zh.svg"></a>
  <a href="README_en.md"><img alt="English" src=".github/badges/language-en.svg"></a>
  <a href="CHANGELOG.md"><img alt="更新日志" src=".github/badges/changelog-zh.svg"></a>
  <a href="https://github.com/JMC2002/SlayTheSpire2_SpireGuard/releases"><img alt="Releases" src=".github/badges/releases.svg"></a>
<!-- code-stats:start -->
<!-- code-stats:end -->
</p>

# SpireGuard
##  0. 安装

### Mod本体安装
Steam版本直接在创意工坊订阅即可（暂未开放）

其他版本可以自行编译，或者在[📦 Releases](https://github.com/JMC2002/SlayTheSpire2_SpireGuard/releases)界面下载.zip后解压到游戏安装目录下的Mods
目录下（没有就新建一个）

### 前置安装
**此外，本模组强依赖于模组[JmcModLib](https://github.com/JMC2002/SlayTheSpire2_JmcModLib/releases)**，安装方法同上

安装完成后的目录结构如下：

```sh
-- Slay the Spire 2
    |-- SlayTheSpire2.exe
        |-- mods
             |-- JmcModLib
             |-- SpireGuard
                  |-- SpireGuard.dll
                  |-- SpireGuard.pck
                  |-- SpireGuard.json
```

### 存档迁移
> 当你第一次安装MOD，游戏会默认将开启Mod的存档与没开启的隔离，可以按下面的方法迁移存档：

在安装好MOD后第一次打开游戏会询问是否启用MOD，启用并再次打开游戏一次后，退出游戏，将`%appdata%\SlayTheSpire2\steam\`下面的数字文件夹下的你对应的存档文件粘贴到该文件夹的`modded`文件夹中，以同步使用MOD前后的存档

---
## 🧠 1. 简介
SpireGuard 用于限制《杀戮尖塔 2》联机局中的控制台权限。主机安装后会拒收客机发来的控制台网络动作；自己作为客机时默认仍可使用控制台，也可以通过设置项主动禁止本机控制台。

[演示视频（B站）](待定)

[Github仓库](https://github.com/JMC2002/SlayTheSpire2_SpireGuard)
## ⚙️ 2. 功能
- 主机端拒收客机绕过本地限制发送的控制台网络动作。
- 主机拦截客机控制台网络动作后，可弹出提示框提示拦截行为。
- 可选：进入联机运行后，自己作为客机时禁止本机主动打开或提交控制台命令。
- 可选：检测到其他玩家使用控制台网络动作时弹出提示框。
- 提供设置项，可以随时启用或停用保护逻辑、客机本机限制和弹窗提醒。
- 仅需主机安装即可权威拦截客机控制台网络动作。
 
## 🔔 3. 提醒
- **本模组强依赖于模组[JmcModLib](https://github.com/JMC2002/SlayTheSpire2_JmcModLib/releases)**
- 作为客机安装时，只能检测已经同步到本机的其他玩家控制台动作，无法替未安装的主机拦截其他玩家。
 
## 🧩 4. 兼容性
- 由于游戏处于EA阶段，可能会随着游戏版本更新而失效

## 🧭 5. TODO
- 根据后续游戏版本变化继续跟进控制台和联机同步入口。

**如果你喜欢这个 Mod 的话，希望可以点一个star~**
