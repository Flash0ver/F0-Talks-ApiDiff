export function show(message) {
    window.alert(message);
}

export function write(data) {
    console.info(data);
}

export function read(text, defaultText) {
    return prompt(text, defaultText)
}

export async function callback() {
    const { getAssemblyExports } = await globalThis.getDotnetRuntime(0);
    var exports = await getAssemblyExports("F0.Talks.ApiDiff.Net70.BlazorApp.dll");

    var message = exports.F0.Pages.JavaScriptInteropPage.GetMessage();
    document.getElementById("result").innerText = message;
}
