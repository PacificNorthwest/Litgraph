export default {
  createCssSelector: (selector: string, style: string) => {
    var css = document.createElement('style');
    css.type = 'text/css';
    css.innerHTML = `.${selector} ${style}`;
    (document.head as any).appendChild(css);
  }
}